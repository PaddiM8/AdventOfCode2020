using System;
using System.Collections.Generic;
using System.Linq;

class Day10 : IDay
{
    public readonly List<int> _adapters;

    public Day10(string[] lines)
    {
        _adapters = lines.Select(int.Parse).OrderBy(x => x).ToList();
    }

    public object Part1()
    {
        int differenceBy1 = 0;
        int differenceBy3 = 1;
        int previous = 0;
        foreach (int adapter in _adapters)
        {
            if (adapter - previous == 1) differenceBy1++;
            if (adapter - previous == 3) differenceBy3++;
            previous = adapter;
        }

        return differenceBy1 * differenceBy3;
    }

    public object Part2()
    {
        long arrangements = 1;
        Console.WriteLine(string.Join(", ", _adapters));
        for (int i = 0; i < _adapters.Count;)
        {
            int j = i;
            int previous;
            do
            {
                previous = _adapters[j++];
            }
            while (j < _adapters.Count && _adapters[j] - previous == 1);

            if (j - i > 1)
            {
                if (i == 0) j++;
                arrangements *= j - i;
                i = j;
            }
            else i++;
        }

        return arrangements;
    }
}