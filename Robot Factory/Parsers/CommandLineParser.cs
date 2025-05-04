using Robot_Factory.Errors;
using Robot_Factory.Models.Types;
using static System.Text.RegularExpressions.Regex;

namespace Robot_Factory.Parsers;

internal static class CommandLineParser
{
    private static readonly List<Command> Commands = Enum.GetValues<Command>().ToList();

    public static (Command command, List<Order> orders) Parse(string userInput)
    {
        var commandText = ExtractCommandText(userInput.Trim());
        var command = commandText.ToCommand();
        var arguments = ExtractArguments(userInput.Trim());
        return (command, arguments);
    }

    private static string ExtractCommandText(string userInput)
    {
        return Split(userInput, @"(?<=\w)\s+(?=\w)")
            .First();
    }

    private static List<Order> ExtractArguments(string userInput)
    {
        var arguments = ExtractArgumentsText(userInput);
        if (string.IsNullOrWhiteSpace(arguments))
            return [];

        var ordersText = SplitOrders(arguments);
        try
        {
            return ParseOrders(ordersText);
        }
        catch (Exception e)
        {
            CommandLineError.Display(e.Message);
            return [];
        }
    }

    private static string? ExtractArgumentsText(string userInput)
    {
        var firstSpaceIndex = userInput.IndexOf(' ');

        if (firstSpaceIndex == -1 || firstSpaceIndex == userInput.Length - 1)
            return null;

        return userInput[(firstSpaceIndex + 1)..].Trim();
    }

    private static List<string> SplitOrders(string arguments)
    {
        var orders = Split(arguments, @"(?<=\w),(?=\s*\w)").ToList();
        return orders.Select(order =>
            !string.IsNullOrEmpty(order) && order[^1] == ',' ? order[..^1] : order
        ).Select(order => order.Trim()).ToList(); ;
    }

    private static List<Order> ParseOrders(List<string> ordersText)
    {

        var parseOrder = new Func<string, Order>(orderText =>
        {
            var parts = orderText.Split(" ");
            return new Order(
                int.Parse(parts[0]),
                parts[1].ToRobotType()
            );
        });

        var mergeOrdersByType = new Func<IGrouping<RobotType, Order>, Order>(group =>
        {
            return new Order(
                group.Sum(order => order.Quantity), 
                group.Key);
        });


        return ordersText
            .Select(parseOrder)
            .GroupBy(order => order.RobotType)
            .Select(mergeOrdersByType)
            .ToList();
    }
}