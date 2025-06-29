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
            CoreType.Cm1 => "Core_CM1",
            CoreType.Cd1 => "Core_CD1",
            CoreType.Ci1 => "Core_CI1",
            _ => throw new ArgumentOutOfRangeException($"Type {type} is not an available core")
        };
    }

    public static List<CoreSystem> GetSupportedSystemTypes(this CoreType type)
    {
        return type switch
        {
            CoreType.Cm1 => [CoreSystem.Sb1],
            CoreType.Cd1 => [CoreSystem.Sb1],
            CoreType.Ci1 => [CoreSystem.Sb1],
            _ => throw new ArgumentOutOfRangeException($"Type {type} is not an available core")
        };
    }
}