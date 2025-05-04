namespace Robot_Factory.Models.Types;

internal enum CoreSystem
{
    Sb1
}

internal static class SystemExtensions
{
    public static string Stringify(this CoreSystem system)
    {
        return system switch
        {
            CoreSystem.Sb1 => "System_SB1",
            _ => throw new ArgumentOutOfRangeException($"{system} is not supported")
        };
    }

    public static CoreSystem ToSystem(this string system)
    {
        return system switch
        {
            "System_SB1" => CoreSystem.Sb1,
            _ => throw new ArgumentException($"{system} is not a valid type of system")
        };
    }
}