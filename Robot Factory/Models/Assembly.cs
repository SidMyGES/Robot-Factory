using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal struct Assembly(string? name, object part1, object part2)
{
    public string? Name { get; private set; } = name;
    public object Part1 { get; private set; } = part1;
    public object Part2 { get; private set; } = part2;

    public bool IsComplete()
    {
        if (Part1 == null || Part2 == null)
            return false;

        var count = CountParts(Part1) + CountParts(Part2);
        return count >= 4;
    }

    private static int CountParts(object part)
    {
        if (part is Assembly nested)
        {
            return CountParts(nested.Part1) + CountParts(nested.Part2);
        }

        return 1;
    }
}