namespace Robot_Factory.Models.Types;

internal enum CoreType
{
    Cm1,
    Cd1,
    Ci1
}

internal static class CoreTypeExtension
{
    public static string Stringify(this CoreType type)
    {
        return type switch
        {
            CoreType.Cm1 => "CM1",
            CoreType.Cd1 => "CD1",
            CoreType.Ci1 => "CI1",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} is not an available core")
        };
    }
}