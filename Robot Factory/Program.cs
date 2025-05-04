using Robot_Factory.Models;
using Robot_Factory.Models.Types;
using Robot_Factory.Services;

namespace Robot_Factory;

internal class Program
{
    public static void Main(string[] args)
    {
        var inventoryService = new InventoryService();
        FillData(inventoryService);
        var orders = new List<Order>
        {
            new(3, RobotType.Xm1),
            new(3, RobotType.Rd1),
            new(1, RobotType.Wi1)
        };

        var userInputService = new CommandService(inventoryService);

        //userInputService.Stocks();
        //userInputService.NeededStocks(orders);
        Console.ReadLine();

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
        foreach (var _ in Enumerable.Range(1, 5))
        {
            AddItem<RobotType, Robot>(
                type => new Robot(type),
                inventoryService.AddRobot);

            AddItem<CoreType, Core>(
                type => new Core(type),
                inventoryService.AddCore);

            AddItem<ArmType, Arms>(
                type => new Arms(type),
                inventoryService.AddArms);

            AddItem<LegType, Legs>(
                type => new Legs(type),
                inventoryService.AddLegs);

            AddItem<GeneratorType, Generator>(
                type => new Generator(type),
                inventoryService.AddGenerator);
        }
    }
}