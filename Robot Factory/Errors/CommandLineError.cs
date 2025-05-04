namespace Robot_Factory.Errors;

internal static class CommandLineError
{
    public static void Display(string message)
    {
        Console.WriteLine("ERROR " + message);
    }
}