using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JachowskiOS.System
{
    public static class WriteMessage
    {
        public static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(error);
        }
        public static void WriteWarn(string warn)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[Warning]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(warn);
        }
        public static void WriteInfo(string info)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[Info]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(info);
        }

        public static string CenterText(string text)
        {
            int consoleWidth = 90;
            int padding = (consoleWidth - text.Length) / 2;
            string centeredText = text.PadLeft(padding + text.Length).PadRight(consoleWidth);
            return centeredText;

        }
        public static void WriteOK(string massage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[OK] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(massage);
        }
    }  
}
