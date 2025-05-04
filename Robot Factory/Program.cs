using Robot_Factory.Models;
using Robot_Factory.Models.Types;
using Robot_Factory.Parsers;
using Robot_Factory.Services;

namespace Robot_Factory;

internal class Program
{
    public static void Main(string[] args)
    {
        var inventoryService = new InventoryService();
        FillData(inventoryService);
        var commandService = new CommandService(inventoryService);
        CommandDispatcher.Initialize(commandService);

        RunCommandLoop();
    }

    public static void RunCommandLoop()
    {
        while (true)
        {
            Console.WriteLine("Enter a command (STOCKS, INSTRUCTIONS, NEEDED_STOCKS, VERIFY, PRODUCE, EXIT):");
            var userInput = Console.ReadLine()?.Trim();

            if (userInput?.ToUpper() == "EXIT")
                break;

            var parsed = CommandLineParser.Parse(userInput ?? "");
            CommandDispatcher.Dispatch(parsed);
        }
    }

    public static void AddItem<TEnum, TItem>(Func<TEnum,TItem> createItem, 
        Action<TItem> addToList) 
        where TEnum : struct, Enum
    {
        foreach (var type in Enum.GetValues<TEnum>())
        {
            var item = createItem(type);
            addToList(item);
        }
    }

    public static void FillData(InventoryService inventoryService)
    {
        foreach (var _ in Enumerable.Range(1, 200))
        {
            AddItem<RobotType, Robot>(
                type => new Robot(type),
                inventoryService.AddRobot);

            AddItem<CoreType, Core>(
                type => new Core(type),
                inventoryService.AddCore);

            AddItem<ArmsType, Arms>(
                type => new Arms(type),
                inventoryService.AddArms);

            AddItem<LegsType, Legs>(
                type => new Legs(type),
                inventoryService.AddLegs);

            AddItem<GeneratorType, Generator>(
                type => new Generator(type),
                inventoryService.AddGenerator);
        }
    }
}