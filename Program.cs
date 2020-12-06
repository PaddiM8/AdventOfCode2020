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
            var constructor = day.GetConstructors()[0];
            var parameterType = constructor.GetParameters()[0].ParameterType;
            string inputFile = $"input/{day.Name[3..]}.txt";
            IDay instance;

            if (parameterType == typeof(string))
            {
                string input = File.ReadAllText(inputFile);
                instance = (IDay)constructor.Invoke(new[] { input });
            }
            else if (parameterType == typeof(string[]))
            {
                var inputLines = File.ReadAllLines(inputFile);
                instance = (IDay)constructor.Invoke(new[] { inputLines });
            }
            else if (parameterType == typeof(string[][]))
            {
                var input = File.ReadAllText(inputFile);
                var inputSections = input.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
                var lines = inputSections.Select(x => x.Split('\n')).ToArray();
                instance = (IDay)constructor.Invoke(new[] { lines });
            }
            else
            {
                throw new Exception($"Missing constructor in {day.Name}");
            }

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
