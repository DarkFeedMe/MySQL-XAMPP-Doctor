using Microsoft.VisualBasic.FileIO;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MySQL_XAMPP_Doctor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            Console.WriteLine($"Starting {exePath}");
            string mysqlPath = Path.GetDirectoryName(exePath.TrimEnd(Path.DirectorySeparatorChar));

            if (Path.GetFileName(mysqlPath).ToLower() != "mysql")
            {
                Console.WriteLine("Executable must be in 'mysql' folder. Enter correct mysql folder path or press Enter to exit:");
                string userPath = Console.ReadLine();
                if (string.IsNullOrEmpty(userPath))
                {
                    Console.WriteLine("Exiting program.");
                    return;
                }
                if (!Directory.Exists(userPath) || Path.GetFileName(userPath).ToLower() != "mysql")
                {
                    Console.WriteLine("Invalid mysql folder path. Press any key to exit.");
                    Console.ReadKey();
                    return;
                }
                mysqlPath = userPath;
                Console.WriteLine($"Using provided mysql folder path: {mysqlPath}");
            }
            else
            {
                Console.WriteLine($"Detected mysql folder: {mysqlPath}");
            }

            string dataPath = Path.Combine(mysqlPath, "data");
            string dataOldPath = Path.Combine(mysqlPath, "data_old");
            string backupPath = Path.Combine(mysqlPath, "backup");
            string ibdata1OldPath = Path.Combine(dataOldPath, "ibdata1");
            string ibdata1NewPath = Path.Combine(dataPath, "ibdata1");

            try
            {
                if (Directory.Exists(dataPath))
                {
                    Console.WriteLine($"Renaming folder {dataPath} to {dataOldPath}...");
                    Directory.Move(dataPath, dataOldPath);
                    Console.WriteLine("Folder renamed successfully.");
                }
                else
                {
                    Console.WriteLine($"Folder {dataPath} does not exist, skipping rename.");
                }

                if (Directory.Exists(backupPath))
                {
                    Console.WriteLine($"Copying folder {backupPath} to {dataPath}...");
                    CopyDirectory(backupPath, dataPath);
                    Console.WriteLine("Folder copied successfully.");
                }
                else
                {
                    Console.WriteLine($"Backup folder {backupPath} does not exist, skipping copy.");
                }

                if (Directory.Exists(dataOldPath))
                {
                    Console.WriteLine("Copying database folders from data_old to data (excluding mysql, performance_schema, phpmyadmin, test)...");
                    string[] excludedFolders = { "mysql", "performance_schema", "phpmyadmin", "test" };
                    int copiedFolders = 0;
                    foreach (string folder in Directory.GetDirectories(dataOldPath))
                    {
                        string folderName = Path.GetFileName(folder);
                        if (!excludedFolders.Contains(folderName.ToLower()))
                        {
                            string destFolder = Path.Combine(dataPath, folderName);
                            Console.WriteLine($"Copying folder {folderName}...");
                            CopyDirectory(folder, destFolder);
                            Console.WriteLine($"Folder {folderName} copied successfully.");
                            copiedFolders++;
                        }
                    }
                    Console.WriteLine($"{copiedFolders} database folder(s) copied.");
                }
                else
                {
                    Console.WriteLine($"Folder {dataOldPath} does not exist, skipping database folder copy.");
                }

                if (File.Exists(ibdata1OldPath))
                {
                    Console.WriteLine($"Copying file {ibdata1OldPath} to {ibdata1NewPath}...");
                    File.Copy(ibdata1OldPath, ibdata1NewPath, true);
                    Console.WriteLine("ibdata1 file copied successfully.");
                }
                else
                {
                    Console.WriteLine($"File {ibdata1OldPath} does not exist, skipping copy.");
                }

                Console.WriteLine("All operations completed successfully.");
                if (Directory.Exists(dataOldPath))
                {
                    Console.WriteLine($"Do you want to send the {dataOldPath} folder to the Recycle Bin? (y/n)");
                    string response = Console.ReadLine().Trim().ToLower();
                    if (response == "y")
                    {
                        Console.WriteLine($"Sending {dataOldPath} to Recycle Bin...");
                        FileSystem.DeleteDirectory(dataOldPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                        Console.WriteLine("Folder sent to Recycle Bin successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Keeping {dataOldPath} folder.");
                    }
                }
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}. Press any key to exit.");
                Console.ReadKey();
            }
        }

        static void CopyDirectory(string sourceDir, string destDir)
        {
            if (!Directory.Exists(sourceDir))
                return;

            Directory.CreateDirectory(destDir);
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }
            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }
    }
}
