using System;
using System.Linq;

class Day3 : IDay
{
    private readonly string[] _lines;

    public Day3(string[] lines)
    {
        _lines = lines;
    }

    public object Part1()
    {
        return GetTreeCountForSlope(3, 1);
    }

    public object Part2()
    {
        (int right, int down)[] slopes =
        {
            (1, 1),
            (3, 1),
            (5, 1),
            (7, 1),
            (1, 2),
        };

        return slopes.Aggregate(1, (val, slope) =>
                GetTreeCountForSlope(slope.right, slope.down) * val);
    }

    private int GetTreeCountForSlope(int slopeRight, int slopeDown)
    {
        int x = 0;
        int treeCount = 0;
        int width = _lines[0].Length;
        for (int y = 0; y < _lines.Length; y += slopeDown)
        {
            if (x >= width) x -= width;
            if (_lines[y][x] == '#') treeCount++;
            x += slopeRight;
        }

        return treeCount;
    }
}
