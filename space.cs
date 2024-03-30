using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JachowskiOS
{
    public static class DiskSpaceCommand
    {
        public static void DisplayAvailableDiskSpace()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Drive {drive.Name}");
                    Console.WriteLine($"  Available space: {BytesToGigabytes(drive.AvailableFreeSpace):F2} GB");
                }
            }
        }

        private static double BytesToGigabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f / 1024f;
        }
    }
}


