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

        Data.System.Register(CoreSystem.Sb1, PartCategory.General);    
        Data.System.Register(CoreSystem.Sm1, PartCategory.Military);   
        Data.System.Register(CoreSystem.Sd1, PartCategory.Domestic);   
        Data.System.Register(CoreSystem.Si1, PartCategory.Industrial);


        var categories = new[] {
            PartCategory.Domestic,
            PartCategory.Industrial,
            PartCategory.Military,
            PartCategory.General
        };



        foreach (var category in categories)
        {
            foreach (var _ in Enumerable.Range(1, 50))
            {
                AddItem<CoreType, Core>(
                    type => new Core(type, category),
                    inventoryService.AddCore);

                AddItem<ArmsType, Arms>(
                    type => new Arms(type, category),
                    inventoryService.AddArms);

                AddItem<LegsType, Legs>(
                    type => new Legs(type, category),
                    inventoryService.AddLegs);

                AddItem<GeneratorType, Generator>(
                    type => new Generator(type, category),
                    inventoryService.AddGenerator);
            }
        }

        foreach (var _ in Enumerable.Range(1, 200))
        {
            AddItem<RobotType, Robot>(
                type => new Robot(type),
                inventoryService.AddRobot);
        }
    }
}