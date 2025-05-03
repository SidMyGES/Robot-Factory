namespace Robot_Factory.Models.Types;

internal enum LegsType
{
    Lm1,
    Ld1,
    Li1
}

internal static class LegsTypeExtension
{
    public static string Stringify(this LegsType type)
    {
        return type switch
        {
            LegsType.Lm1 => "LM1",
            LegsType.Ld1 => "LD1",
            LegsType.Li1 => "LI1",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} is not an available Leg")
        };
    }
}