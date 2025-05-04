namespace Robot_Factory.Errors;

internal static class CommandLineError
{
    public static void Display(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ERROR ");
        Console.ResetColor();
        Console.WriteLine(message);
    }
}