
namespace Robot_Factory.Models.Types;

internal enum ArmsType
{
    Am1,
    Ad1,
    Ai1
}

internal static class ArmsTypeExtension
{
    public static string Stringify(this ArmsType type)
    {
        return type switch
        {
            ArmsType.Am1 => "Arms_AM1",
            ArmsType.Ad1 => "Arms_AD1",
            ArmsType.Ai1 => "Arms_AI1",
            _ => throw new ArgumentOutOfRangeException($"Type {type} is not an available Arms")
        };
    }

    public static ArmsType ToArmsType(this string type)
    {
        return type switch
        {
            "Arms_AM1" => ArmsType.Am1,
            "Arms_AD1" => ArmsType.Ad1,
            "Arms_AI1" => ArmsType.Ai1,
            _ => throw new ArgumentException($"{type} is not a valid type of arms")
        };
    }
}