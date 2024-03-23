using Cosmos.System.FileSystem;
using JachowskiOS.System;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace JachowskiOS
{
    public class Kernel : Sys.Kernel
    {
        public static string Version = "1.0.0";
        public static string Path = @"0:\";
        public static CosmosVFS fs;
        protected override void BeforeRun()
        {
            Console.SetWindowSize(90, 30);
            Console.OutputEncoding = Cosmos.System.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437);
            fs = new Cosmos.System.FileSystem.CosmosVFS();
            Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Booting JachowskiOS " + Version);
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected override void Run()
        {
            Console.Write(Path + ">");
            var command = Console.ReadLine();
            ConsoleCommands.RunCommand(command);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
