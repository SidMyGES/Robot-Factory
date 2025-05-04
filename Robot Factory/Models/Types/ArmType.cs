
namespace Robot_Factory.Models.Types;

internal enum ArmType
{
    Am1,
    Ad1,
    Ai1
}

internal static class ArmTypeExtension
{
    public static string Stringify(this ArmType type)
    {
        return type switch
        {
            ArmType.Am1 => "Arms_AM1",
            ArmType.Ad1 => "Arms_AD1",
            ArmType.Ai1 => "Arms_AI1",
            _ => throw new ArgumentOutOfRangeException($"Type {type} is not an available Arm")
        };
    }
}