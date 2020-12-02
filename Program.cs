using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

class Program
{
    public static void Main(string[] args)
    {
        var days = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.GetInterface("IDay") != null);

        if (args.Length > 0)
        {
            // Only run the day specified with a command line argument
            days = days.Where(day => day.Name == "Day" + args[0]);
        }

        foreach (var day in days)
        {
            var inputLines = File.ReadAllLines($"input/{day.Name[3..]}.txt");
            var instance = (IDay)Activator.CreateInstance(day, new[] { inputLines });

            WriteLineWithColor($"~= {day.Name} =~", ConsoleColor.Blue);
            WriteLineWithColor("------------", ConsoleColor.Blue);

            WriteLineWithColor("1: ", ConsoleColor.Green);
            Console.WriteLine(instance.Part1());

            WriteLineWithColor("2: ", ConsoleColor.Green);
            Console.WriteLine(instance.Part2());

            Console.WriteLine();
        }
    }

    private static void WriteLineWithColor(string input, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(input);
        Console.ResetColor();
    }
}
