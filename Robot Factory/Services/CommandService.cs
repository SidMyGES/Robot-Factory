
using Robot_Factory.Models;
using Robot_Factory.Models.Types;
using Robot_Factory.Utils;

namespace Robot_Factory.Services;

internal class CommandService(InventoryService inventoryService)
{
    internal void Stocks()
    {
        DisplayUtils.Print<RobotType, Robot> (
            inventoryService.GetRobotsByType);

        DisplayUtils.Print<CoreType, Core>(
            inventoryService.GetCoresByType);

        DisplayUtils.Print<ArmType, Arms>(
            inventoryService.GetArmsByType);

        DisplayUtils.Print<LegType, Legs>(
            inventoryService.GetLegsByType);

        DisplayUtils.Print<GeneratorType, Generator>(
            inventoryService.GetGeneratorsByType);
    }

    internal void NeededStocks(List<Order> orders)
    {
        var totalParts = new Dictionary<string, int>();

        foreach (var order in orders)
        {
            var robotType = order.RobotType;
            var robotQuantity = order.Quantity;

            DisplayUtils.Print(robotType.Stringify() + " :", robotQuantity);

            var core = robotType.GetCompatibleCore();
            DisplayUtils.Print(core.Type.Stringify(), robotQuantity * core.Quantity);
            AddPart(core.Type.Stringify(), robotQuantity * core.Quantity, totalParts);

            var generator = robotType.GetCompatibleGenerator();
            DisplayUtils.Print(generator.Type.Stringify(), robotQuantity * generator.Quantity);
            AddPart(generator.Type.Stringify(), robotQuantity * generator.Quantity, totalParts);

            var arms = robotType.GetCompatibleArms();
            DisplayUtils.Print(arms.Type.Stringify(), robotQuantity * arms.Quantity);
            AddPart(arms.Type.Stringify(), robotQuantity * arms.Quantity, totalParts);

            var legs = robotType.GetCompatibleLegs();
            DisplayUtils.Print(legs.Type.Stringify(), robotQuantity * legs.Quantity);
            AddPart(legs.Type.Stringify(), robotQuantity * legs.Quantity, totalParts);

            Console.WriteLine();
        }

        Console.WriteLine("Total :");
        foreach (var part in totalParts)
        {
            DisplayUtils.Print(part.Key, part.Value);
        }
    }


    private void AddPart(string key, int value, Dictionary<string, int> map)
    {
        if(!map.TryAdd(key, value))
        {
            map[key] += value;
        }
    }

}