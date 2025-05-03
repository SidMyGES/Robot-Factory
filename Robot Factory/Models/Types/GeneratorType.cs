namespace Robot_Factory.Models.Types;

public enum GeneratorType
{
    Gm1,
    Gd1,
    Gi1
}

internal static class GeneratorTypeExtension
{
    public static string Stringify(this GeneratorType type)
    {
        return type switch
        {
            GeneratorType.Gm1 => "GM1",
            GeneratorType.Gd1 => "GD1",
            GeneratorType.Gi1 => "GI1",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} is not an available Generator")
        };
    }
}