using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

class Day1 : IDay
{
    private readonly IEnumerable<int> _numbers;

    public Day1(string[] inputLines)
    {
        _numbers = inputLines.Select(int.Parse);
    }

    public object Part1()
    {
        foreach (int x in _numbers)
        {
            foreach (int y in _numbers)
            {
                if (x + y == 2020) return x * y;
            }
        }

        return null;
    }

    public object Part2()
    {
        foreach (var x in _numbers)
        {
            foreach (var y in _numbers)
            {
                foreach (var z in _numbers)
                {
                    if (x + y + z == 2020) return x * y * z;
                }
            }
        }

        return null;
    }
}
