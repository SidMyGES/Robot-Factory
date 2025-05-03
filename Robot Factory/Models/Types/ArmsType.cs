
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
            ArmsType.Am1 => "AM1",
            ArmsType.Ad1 => "AD1",
            ArmsType.Ai1 => "AI1",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} is not an available Arm")
        };
    }
}