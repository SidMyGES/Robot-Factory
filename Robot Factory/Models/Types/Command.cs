namespace Robot_Factory.Models.Types;

internal enum Command
{
    Stocks,
    NeededStocks,
    Instructions,
    Verify,
    Produce,
    Error
}

internal static class CommandExtensions
{
    public static string Stringify(this Command command)
    {
        return command switch
        {
            Command.Stocks => "STOCKS",
            Command.NeededStocks => "NEEDED_STOCKS",
            Command.Instructions => "INSTRUCTIONS",
            Command.Verify => "VERIFY",
            Command.Produce => "PRODUCE",
            _ => "ERROR"
        };
    }

    public static bool HasArguments(this Command command)
    {
        return command switch
        {
            Command.NeededStocks => true,
            Command.Instructions => true,
            Command.Verify => true,
            Command.Produce => true,
            _ => false
        };
    }

    public static Command ToCommand(this string commandString)
    {
        return commandString.ToUpper() switch
        {
            "STOCKS" => Command.Stocks,
            "NEEDED_STOCKS" => Command.NeededStocks,
            "INSTRUCTIONS" => Command.Instructions,
            "VERIFY" => Command.Verify,
            "PRODUCE" => Command.Produce,
            _ => Command.Error
        };
    }

}