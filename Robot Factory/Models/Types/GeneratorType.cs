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
            GeneratorType.Gm1 => "Generator_GM1",
            GeneratorType.Gd1 => "Generator_GD1",
            GeneratorType.Gi1 => "Generator_GI1",
            _ => throw new ArgumentOutOfRangeException($"Type {type} is not an available Generator")
        };
    }

    public static GeneratorType ToGeneratorType(this string type)
    {
        return type switch
        {
            "Generator_GM1" => GeneratorType.Gm1,
            "Generator_GD1" => GeneratorType.Gd1,
            "Generator_GI1" => GeneratorType.Gi1,
            _ => throw new ArgumentException($"{type} is not a valid type of generator")
        };
    }
}