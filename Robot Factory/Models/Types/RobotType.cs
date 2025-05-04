namespace Robot_Factory.Models.Types;

internal enum RobotType
{
    Xm1,
    Rd1,
    Wi1
}


internal static class RobotTypeExtension
{
    public static string Stringify(this RobotType type)
    {
        return type switch
        {
            RobotType.Xm1 => "XM-1",
            RobotType.Rd1 => "RD-1",
            RobotType.Wi1 => "WI-1",
            _ => throw new ArgumentOutOfRangeException($"{type} is not a declared robot type")
        };
    }

    public static RobotType ToRobotType(this string type)
    {
        return type switch
        {
            "XM-1" => RobotType.Xm1,
            "RD-1" => RobotType.Rd1,
            "WI-1" => RobotType.Wi1,
            _ => throw new ArgumentException($"{type} is not a valid type of robot")
        };
    }

    public static PartInfo<CoreType> GetCompatibleCore(this RobotType type)
    {
        return type switch
        {
            RobotType.Xm1 => new PartInfo<CoreType>(CoreType.Cm1, 1),
            RobotType.Rd1 => new PartInfo<CoreType>(CoreType.Cd1, 1),
            RobotType.Wi1 => new PartInfo<CoreType>(CoreType.Ci1, 1),
            _ => throw new ArgumentOutOfRangeException($"{type} is not a declared robot type")
        };
    }


    public static PartInfo<GeneratorType> GetCompatibleGenerator(this RobotType type)
    {
        return type switch
        {
            RobotType.Xm1 => new PartInfo<GeneratorType>(GeneratorType.Gm1, 1),
            RobotType.Rd1 => new PartInfo<GeneratorType>(GeneratorType.Gd1, 1),
            RobotType.Wi1 => new PartInfo<GeneratorType>(GeneratorType.Gi1, 1),
            _ => throw new ArgumentOutOfRangeException($"{type} is not a declared robot type")
        };
    }


    public static PartInfo<ArmsType> GetCompatibleArms(this RobotType type)
    {
        return type switch
        {
            RobotType.Xm1 => new PartInfo<ArmsType>(ArmsType.Am1, 1),
            RobotType.Rd1 => new PartInfo<ArmsType>(ArmsType.Ad1, 1),
            RobotType.Wi1 => new PartInfo<ArmsType>(ArmsType.Ai1, 1),
            _ => throw new ArgumentOutOfRangeException($"{type} is not a declared robot type")
        };
    }


    public static PartInfo<LegsType> GetCompatibleLegs(this RobotType type)
    {
        return type switch
        {
            RobotType.Xm1 => new PartInfo<LegsType>(LegsType.Lm1, 1),
            RobotType.Rd1 => new PartInfo<LegsType>(LegsType.Ld1, 1),
            RobotType.Wi1 => new PartInfo<LegsType>(LegsType.Li1, 1),
            _ => throw new ArgumentOutOfRangeException($"{type} is not a declared robot type")
        };
    }

}