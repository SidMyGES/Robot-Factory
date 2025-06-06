﻿namespace Robot_Factory.Models.Types;

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
            LegsType.Lm1 => "Legs_LM1",
            LegsType.Ld1 => "Legs_LD1",
            LegsType.Li1 => "Legs_LI1",
            _ => throw new ArgumentOutOfRangeException($"Type {type} is not an available Legs")
        };
    }

    public static LegsType ToLegsType(this string type)
    {
        return type switch
        {
            "Legs_LM1" => LegsType.Lm1,
            "Legs_LD1" => LegsType.Ld1,
            "Legs_LI1" => LegsType.Li1,
            _ => throw new ArgumentException($"{type} is not a valid type of legs")
        };
    }
}