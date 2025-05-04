namespace Robot_Factory.Models.Types;

internal enum LegType
{
    Lm1,
    Ld1,
    Li1
}

internal static class LegTypeExtension
{
    public static string Stringify(this LegType type)
    {
        return type switch
        {
            LegType.Lm1 => "Legs_LM1",
            LegType.Ld1 => "Legs_LD1",
            LegType.Li1 => "Legs_LI1",
            _ => throw new ArgumentOutOfRangeException($"Type {type} is not an available Leg")
        };
    }
}