using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

class Day1 : IDay
{
    private readonly HashSet<int> _numbers;

    public Day1(string[] inputLines)
    {
        _numbers = new HashSet<int>(inputLines.Select(int.Parse));
    }

    public object Part1()
    {
        foreach (int x in _numbers)
        {
            if (_numbers.TryGetValue(2020 - x, out int y))
                return x * y;
        }

        return null;
    }

    public object Part2()
    {
        foreach (var x in _numbers)
        {
            foreach (var y in _numbers)
            {
                if (_numbers.TryGetValue(2020 - x - y, out int z))
                    return x * y * z;
            }
        }

        return null;
    }
}
