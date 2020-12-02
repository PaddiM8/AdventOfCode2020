using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

record PasswordInfo(int Num1, int Num2, char RequiredChar, string Password);

class Day2 : IDay
{
    private readonly IEnumerable<PasswordInfo> _lines;

    public Day2(string[] inputLines)
    {
        _lines = inputLines.Select(x =>
        {
            var parts = x.Split('-', ' ', ':');
            return new PasswordInfo(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                parts[2][0],
                parts[4]
            );
        });
    }

    public object Part1()
    {
        int validPasswords = 0;
        foreach (var line in _lines)
        {
            int charCount = line.Password.Where(c => c == line.RequiredChar).Count();
            if (charCount >= line.Num1 &&
                charCount <= line.Num2)
                validPasswords++;
        }

        return validPasswords;
    }

    public object Part2()
    {
        int validPasswords = 0;
        foreach (var line in _lines)
        {
            if (line.Password[line.Num1 - 1] == line.RequiredChar ^
                line.Password[line.Num2 - 1] == line.RequiredChar)
                validPasswords++;
        }

        return validPasswords;
    }
}
