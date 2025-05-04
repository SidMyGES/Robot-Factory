
using Robot_Factory.Errors;
using Robot_Factory.Models.Types;

namespace Robot_Factory.Services;

internal static class CommandDispatcher
{
    private static CommandService _commandService = null!;

    public static void Initialize(CommandService commandService)
    {
        _commandService = commandService;
    }

    public static void Dispatch((Command command, List<Order> orders) input)
    {
        switch (input.command)
        {
            case Command.Stocks:
                _commandService.Stocks();
                break;
            case Command.NeededStocks:
                _commandService.NeededStocks(input.orders);
                break;
            case Command.Error:
                CommandLineError.Display("The command entered is not valid");
                break;
        }
    }
}