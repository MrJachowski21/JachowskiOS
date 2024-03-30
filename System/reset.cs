using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JachowskiOS
{
    public static class ResetSystemCommand
    {
        public static void ResetSystem()
        {
            // Usuń wszystkie pliki z systemu
            try
            {
                string[] files = Directory.GetFiles(Kernel.Path);
                foreach (string file in files)
                {
                    File.Delete(file);
                }

                string[] directories = Directory.GetDirectories(Kernel.Path);
                foreach (string directory in directories)
                {
                    Directory.Delete(directory, true);
                }

                Console.WriteLine("All files deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting files: {ex.Message}");
                return;
            }

            // Następnie zresetuj system
            Console.WriteLine("Resetting the system...");
            Cosmos.System.Power.Reboot();
        }
    }
}