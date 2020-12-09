using System;
using System.Linq;
using System.Collections.Generic;

class Day9 : IDay
{
    private readonly List<long> _lines;
    private const int _preamble = 25;

    public Day9(string[] lines)
    {
        _lines = lines.Select(long.Parse).ToList();
    }

    public object Part1()
    {
        return FindInvalidNumber();
    }

    public object Part2()
    {
        long invalidNumber = FindInvalidNumber();
        for (int i = 0; i < _lines.Count; i++)
        {
            long sum = 0;
            for (int j = i; sum < invalidNumber; j++)
            {
                sum += _lines[j];
                if (sum == invalidNumber)
                {
                    var contiguous = _lines.Skip(i).Take(j - i + 1);
                    return contiguous.Min() + contiguous.Max();
                }
            }
        }

        return 0;
    }

    private long FindInvalidNumber()
    {
        for (int i = _preamble; i < _lines.Count; i++)
        {
            var section = _lines
                .Skip(i - _preamble)
                .Take(_preamble);
            if (!section.Any(x => section.Any(y => x + y == _lines[i])))
                return _lines[i];
        }

        return 0;
    }
}