namespace Robot_Factory.Models.Types;

internal enum CoreSystem
{
    Sb1,
    Sm1,
    Sd1,
    Si1
}

internal static class SystemExtensions
{
    public static string Stringify(this CoreSystem system)
    {
        return system switch
        {
            CoreSystem.Sb1 => "System_SB1",
            CoreSystem.Sm1 => "System_SM1",
            CoreSystem.Sd1 => "System_SD1",
            CoreSystem.Si1 => "System_SI1",
            _ => throw new ArgumentOutOfRangeException(nameof(system), $"{system} is not supported")
        };
    }

    public static CoreSystem ToSystem(this string system)
    {
        return system switch
        {
            "System_SB1" => CoreSystem.Sb1,
            "System_SM1" => CoreSystem.Sm1,
            "System_SD1" => CoreSystem.Sd1,
            "System_SI1" => CoreSystem.Si1,
            _ => throw new ArgumentException($"{system} is not a valid type of system")
        };
    }
}