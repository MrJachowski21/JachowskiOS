using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JachowskiOS.System
{
    public static class Calendar
    {
        public static void ShowCalendar(int year, int month)
        {
            Console.Clear();
            Console.WriteLine($"Calendar for {new DateTime(year, month, 1).ToString("MMMM yyyy")}:");
            Console.WriteLine("============================");
            Console.WriteLine(" Sun Mon Tue Wed Thu Fri Sat");
            Console.WriteLine();

            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;

            // Print leading spaces
            for (int i = 0; i < (int)dayOfWeek; i++)
            {
                Console.Write("    ");
            }

            // Print days
            for (int day = 1; day <= daysInMonth; day++)
            {
                if (day == DateTime.Now.Day && year == DateTime.Now.Year && month == DateTime.Now.Month)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write($"{day,4}");

                if (dayOfWeek == DayOfWeek.Saturday)
                {
                    Console.WriteLine();
                }

                // Reset color after printing current day
                if (day == DateTime.Now.Day && year == DateTime.Now.Year && month == DateTime.Now.Month)
                {
                    Console.ResetColor();
                }

                // Increment day of week
                dayOfWeek = dayOfWeek == DayOfWeek.Saturday ? DayOfWeek.Sunday : dayOfWeek + 1;
            }

            Console.WriteLine();
        }

        public static void ChangeMonth(ref int year, ref int month, bool next)
        {
            if (next)
            {
                month++;
                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }
            else
            {
                month--;
                if (month < 1)
                {
                    month = 12;
                    year--;
                }
            }
        }
    }
}

      

