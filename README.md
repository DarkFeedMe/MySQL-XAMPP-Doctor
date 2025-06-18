MySQL XAMPP Doctor
Hey there! MySQL XAMPP Doctor is your go-to tool for fixing that pesky "MySQL shutdown unexpectedly" error in XAMPP. This simple C# console app, built with .NET Framework 4.8.1, makes it easy to get your MySQL back on track by sorting out the mysql folder in your XAMPP setup. It’s designed to be straightforward, even if you’re not a tech guru, and it keeps your databases safe while working its magic.
What It Does
This tool tackles the MySQL shutdown error by carefully managing your MySQL data folders and files. Here’s the step-by-step 

process:
Checks the Folder: Ensures it’s running from the mysql folder in your XAMPP setup (e.g., C:\xampp\mysql). If it’s not, it’ll ask you to point it to the right place.
Renames data Folder: If a mysql/data folder exists, it renames it to mysql/data_old to keep it safe.
Restores from Backup: If you have a mysql/backup folder, it copies it to create a fresh mysql/data folder. If there’s no backup, it skips this step.
Moves Your Databases: Copies your custom database folders from mysql/data_old to the new mysql/data, skipping system folders like mysql, performance_schema, phpmyadmin, and test.
Grabs the ibdata1 File: Copies the ibdata1 file from mysql/data_old to mysql/data if it exists, as it’s often key to getting MySQL running again.
Cleanup Option: After everything’s done, it asks if you want to send the mysql/data_old folder to the Recycle Bin to free up space.

Each step is clearly explained in the console, so you know exactly what’s happening. If a folder or file is missing, the tool skips that step and lets you know, keeping things smooth and safe.
Why It Helps
The "MySQL shutdown unexpectedly" error in XAMPP often stems from corrupted data in the mysql/data folder. MySQL XAMPP Doctor 
fixes it by:

Starting fresh with the mysql/backup folder.
Bringing over your custom databases from the old data folder.
Restoring the critical ibdata1 file.
Offering to clean up the old, potentially problematic folder.

It’s like giving your MySQL a quick health check to get it running smoothly again while keeping your important databases intact.

How to Use It:

Grab the compiled MySQL XAMPP Doctor.exe
Place it in the mysql folder of your XAMPP installation (e.g., C:\xampp\mysql).

If it’s not in the mysql folder, it’ll prompt you to enter the correct path (e.g., C:\xampp\mysql) or exit.
Follow the console as it walks you through each step, showing what it’s doing (e.g., renaming folders, copying files).
At the end, it’ll ask if you want to send mysql/data_old to the Recycle Bin. Type y for yes or n for no.



Good to Know>

Stop MySQL First: Ensure XAMPP’s MySQL service is stopped (via XAMPP Control Panel) before running the tool.
Backup Your Data: The tool is careful, but always back up your databases before touching MySQL files, just in case.

License
This tool is free for any use. It’s provided as-is, so use it carefully. No guarantees, but it’s here to help!
