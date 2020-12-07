using System;
using System.Collections.Generic;
using System.Linq;

record BagRule(int Amount, string Name);

class Day7 : IDay
{
    private readonly Dictionary<string, IEnumerable<BagRule>> _bagMap = new();

    public Day7(string[] lines)
    {
        foreach (var line in lines)
        {
            var parts = line.Split(new[] { "contain" }, StringSplitOptions.TrimEntries);
            string bagName = parts[0][..^5];
            var containing = parts[1].Split(new[] { ", " }, StringSplitOptions.None)
                .Where(x => x != "no other bags.")
                .Select(x =>
                {
                    int firstSpace = x.IndexOf(' ');
                    return new BagRule
                    (
                        int.Parse(x[..firstSpace]),
                        x[(firstSpace + 1)..x.LastIndexOf(' ')]
                    );
                });

            _bagMap.Add(bagName, containing);
        }
    }

    public object Part1()
    {
        // - 1 to exclude the "shiny gold" bag itself.
        return _bagMap.Count(bag => ContainsBag("shiny gold", bag.Key)) - 1;
    }

    public object Part2()
    {
        return CountNested("shiny gold");
    }

    private bool ContainsBag(string bagName, string search) =>
        bagName == search || _bagMap[search].Any(x => ContainsBag(bagName, x.Name));

    private int CountNested(string bagName) =>
        _bagMap[bagName].Sum(child => (CountNested(child.Name) + 1) * child.Amount);
}