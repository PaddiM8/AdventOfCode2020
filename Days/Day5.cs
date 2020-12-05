using System;
using System.Collections.Generic;
using System.Linq;

class Day5 : IDay
{
    private readonly int[] _seatedIds;

    public Day5(string[] lines)
    {
        _seatedIds = lines
            .Select(ParseBoardingPass)
            .OrderBy(x => x)
            .ToArray();
    }

    public object Part1()
    {
        return _seatedIds.Last();
    }

    public object Part2()
    {
        for (int i = 0; i < _seatedIds.Length; i++)
        {
            int currentId = _seatedIds[i];
            int nextId = _seatedIds[i + 1];
            if (currentId == nextId - 2)
                return currentId + 1;
        }

        return 0;
    }

    private int ParseBoardingPass(string value) =>
        Convert.ToInt32(new string(
            value.Select(c => c == 'B' || c == 'R' ? '1' : '0').ToArray()), 2);
}