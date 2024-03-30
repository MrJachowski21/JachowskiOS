using Cosmos.Core_Asm;
using JachowskiOS.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JachowskiOS.System
{
    public static class ConsoleCommands
    {
        public static void RunCommand(string command)
        {
            string[] words = command.Split(' ');
            if (words.Length > 0)
            {
                if (words[0].ToLower() == "shutdown")
                {
                    ShutdownSystemCommand.ShutdownSystem();
                }
                else if (words[0].ToLower() == "reset")
                {
                    ResetSystemCommand.ResetSystem();
                }
                else if (words[0] == "calculator")
                {
                    Calculator.RunCalculator();
                }
                else if (words[0] == "disc")
                {
                    DiskSpaceCommand.DisplayAvailableDiskSpace();
                }
                else if (words[0].ToLower() == "clock")
                {
                    Clock.Start();
                    return;
                }

                else if (words[0].ToLower() == "time")
                {
                    ShowTime();
                    return;
                }
                else if (words[0] == "info")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(WriteMessage.CenterText("JachowskiOS"));
                    Console.WriteLine(WriteMessage.CenterText(Kernel.Version));
                    Console.WriteLine(WriteMessage.CenterText("Created by Jachowski System "));
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else if (words[0] == "help")
                {
                    Console.WriteLine("Available commands:");
                    Console.WriteLine("- info: Display system information");
                    Console.WriteLine("- format: Format the disk");
                    Console.WriteLine("- space: Check available free space (no available)");
                    Console.WriteLine("- dir: List directories and files in the current directory");
                    Console.WriteLine("- echo <text> > <file>: Write text to a file");
                    Console.WriteLine("- cat <file>: Display the contents of a file");
                    Console.WriteLine("- del <file>: Delete a file");
                    Console.WriteLine("- mkdir <directory>: Create a new directory");
                    Console.WriteLine("- time: see actual time");
                    Console.WriteLine("- clock: stoper");
                    Console.WriteLine("- calculator: open calculator");
                    Console.WriteLine("- disc: Check available free space in GB");
                    Console.WriteLine("- reset: reset system");
                }
                else if (words[0] == "format")
                {
                    if (Kernel.fs.Disks[0].Partitions.Count > 0)
                    {
                        Kernel.fs.Disks[0].DeletePartition(0);
                    }
                    Kernel.fs.Disks[0].Clear();
                    Kernel.fs.Disks[0].CreatePartition(Kernel.fs.Disks[0].Size / (1024 * 1024));
                    Kernel.fs.Disks[0].FormatPartition(0, "FAT32", true);
                    WriteMessage.WriteOK("Succes!");
                    WriteMessage.WriteWarn("JachowskiOS will reboot in 3 seconds!");
                    Thread.Sleep(3000);
                    Cosmos.System.Power.Reboot();
                }
                else if (words[0] == "space")
                {
                    long free = Kernel.fs.GetAvailableFreeSpace(Kernel.Path);
                    Console.Write("Free space: " + free / 1024 + "kb");
                }
                else if (words[0].ToLower() == "notepad")
                {
                    if (words.Length > 1)
                    {
                        string fileName = words[1];
                        Notepad.OpenOrCreateFile(fileName);
                    }
                    else
                    {
                        WriteMessage.WriteError("Please provide a filename.");
                    }
                    return;
                }
                else if (words[0] == "dir")
                {
                    var Directories = Directory.GetDirectories(Kernel.Path);
                    var Files = Directory.GetFiles(Kernel.Path);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Directories (" + Directories.Length + ")");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    for (int i = 0; i < Directories.Length; i++)
                    {
                        Console.WriteLine(Directories[i]);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Files (" + Files.Length + ")");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    for (int i = 0; i < Files.Length; i++)
                    {
                        Console.WriteLine(Files[i]);
                    }
                }
                else if (words[0] == "echo")
                {
                    if (words.Length > 1)
                    {
                        string wholeString = "";
                        for (int i = 1; i < words.Length; i++)
                        {
                            wholeString += words[i] + " ";
                        }
                        int pathIndex = wholeString.LastIndexOf('>');
                        String text = wholeString.Substring(0, pathIndex);
                        String path = wholeString.Substring(pathIndex + 1);
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path;
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        var file_stream = File.Create(path);
                        file_stream.Close();
                        File.WriteAllText(path, text);
                    }
                    else

                        WriteMessage.WriteError("Invalid Syntax!");
                }
                else if (words[0] == "cat")//Wypisujemy zawartość pliku
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path;
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (File.Exists(path))
                        {
                            string text = File.ReadAllText(path);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine(text);
                        }
                        else
                            WriteMessage.WriteError("File " + path + " not found!");
                    }
                    else
                        WriteMessage.WriteError("Invalid Syntax!");

                }
                else if (words[0] == "del")//Usuwamy plik
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path;
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            WriteMessage.WriteOK("Deleted " + path + "!");
                        }
                        else
                            WriteMessage.WriteError("File " + path + " not found!");
                    }
                    else
                        WriteMessage.WriteError("Invalid Syntax!");
                }
                else if (words[0] == "mkdir")//Tworzymy folder
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path;
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        Directory.CreateDirectory(path);
                    }
                    else
                        WriteMessage.WriteError("Invalid Syntax!");
                }
                else if (words[0] == "cd")//Zmieniamy Kernel.Path
                {
                    if (words.Length > 1)
                    {
                        if (words[1] == "..")
                        {
                            if (Kernel.Path != @"0:\")
                            {
                                string tempPath = Kernel.Path.Substring(0, Kernel.Path.Length - 1);
                                Kernel.Path = tempPath.Substring(0, tempPath.LastIndexOf(@"\") + 1);
                                return;
                            }
                            else
                                return;
                        }
                        string path = words[1];
                        if (!path.Contains(@"\"))
                            path = Kernel.Path + path + @"\";
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (!path.EndsWith(@"\"))
                            path += @"\";
                        if (Directory.Exists(path))
                            Kernel.Path = path;
                        else
                            WriteMessage.WriteError("Directory " + path + " not found!");
                    }
                    else
                        Kernel.Path = @"0:\";

                }
                else if (words[0].ToLower() == "calendar")
                {
                    DateTime currentDate = DateTime.Now;
                    int currentYear = currentDate.Year;
                    int currentMonth = currentDate.Month;
                    Calendar.ShowCalendar(currentYear, currentMonth);
                    return;
                }
                else
                {
                    WriteMessage.WriteError("Please enter a valid command!");
                }


            }






        }
        private static void ShowTime()
        {
            DateTime currentTime = DateTime.Now;
            Console.WriteLine($"Current time: {currentTime.ToString("HH:mm:ss")}");
        }
        public static class Clock
        {
            private static bool running = false;
            private static int seconds = 0;
            private static int minutes = 0;
            private static int hours = 0;

            public static void Start()
            {
                running = true;
                seconds = 0;
                minutes = 0;
                hours = 0;
                Console.WriteLine($"Clock started. Time: {hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}");
                UpdateClock();
            }

            public static void Stop()
            {
                running = false;
                Console.WriteLine("Clock stopped.");
            }

            public static void UpdateClock()
            {
                while (running)
                {
                    Thread.Sleep(1000);
                    seconds++;
                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes++;
                        if (minutes == 60)
                        {
                            minutes = 0;
                            hours++;
                            if (hours == 24)
                            {
                                hours = 0;
                            }
                        }
                    }
                    Console.WriteLine($"Time: {hours.ToString("00")}:{minutes.ToString("00")}:{seconds.ToString("00")}");
                }
            }
        }
    }
}

public static class Calculator
{
    public static void RunCalculator()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(WriteMessage.CenterText("Welcome to JachowskiOS Calculator!"));
        Console.WriteLine(WriteMessage.CenterText("Type 'exit' to quit the calculator."));
        Console.WriteLine(WriteMessage.CenterText("Usage: <number> <operation> <number>"));
        Console.WriteLine(WriteMessage.CenterText("Supported operations: +, -, *, /"));

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                WriteMessage.WriteWarn("Exiting calculator...");
                break;
            }

            string[] elements = input.Split(' ');

            if (elements.Length != 3)
            {
                Console.WriteLine("Invalid input! Please provide two numbers and an operation.");
                continue;
            }

            if (!double.TryParse(elements[0], out double num1) || !double.TryParse(elements[2], out double num2))
            {
                Console.WriteLine("Invalid numbers! Please provide valid numerical values.");
                continue;
            }

            string operation = elements[1];

            double result = 0;

            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    if (num2 == 0)
                    {
                        Console.WriteLine("Cannot divide by zero!");
                        continue;
                    }
                    result = num1 / num2;
                    break;
                default:
                    Console.WriteLine("Unsupported operation! Please use '+', '-', '*', or '/'.");
                    continue;
            }

            Console.WriteLine($"Result: {result}");
        }
    }
}