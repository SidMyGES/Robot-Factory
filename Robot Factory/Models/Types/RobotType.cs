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
            RobotType.Xm1 => "XM1",
            RobotType.Rd1 => "RD1",
            RobotType.Wi1 => "WI1",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} is not an available Robot")
        };
    }
}