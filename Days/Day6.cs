using System;
using System.Collections.Generic;
using System.Linq;

class Day6 : IDay
{
    private readonly string[][] _sections;

    public Day6(string[][] sections)
    {
        _sections = sections;
    }

    public object Part1()
    {
        return _sections.Sum(lines =>
            string.Join("", lines)
                .Distinct()
                .Count()
        );
    }

    public object Part2()
    {
        return _sections.Sum(lines =>
            Enumerable.Range('a', 'z')
                .Count(c => lines.All(line => line.Contains((char)c)))
        );
    }
}