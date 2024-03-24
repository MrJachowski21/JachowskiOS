using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace JachowskiOS.System
{
    public static class Notepad
    {
        public static void OpenOrCreateFile(string fileName)
        {
            Console.Clear();
            string filePath = $"{Kernel.Path}{fileName}";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File {fileName} does not exist. Creating a new file...");
                File.Create(filePath).Close();
            }

            Console.WriteLine($"Notepad - Editing file: {fileName}");
            Console.WriteLine("Write your message. Enter ':wq' and press Enter to save and exit.");

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string input;
                do
                {
                    input = Console.ReadLine();
                    if (input != ":wq")
                    {
                        writer.WriteLine(input);
                    }
                } while (input != ":wq");
            }

            WriteMessage.WriteOK($"File {fileName} has been saved and closed.");
        }
    }
}
