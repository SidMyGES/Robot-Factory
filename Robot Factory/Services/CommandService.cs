
using System.Security.AccessControl;
using Robot_Factory.Errors;
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

        DisplayUtils.Print<ArmsType, Arms>(
            inventoryService.GetArmsByType);

        DisplayUtils.Print<LegsType, Legs>(
            inventoryService.GetLegsByType);

        DisplayUtils.Print<GeneratorType, Generator>(
            inventoryService.GetGeneratorsByType);
    }

    internal void NeededStocks(List<Order> orders)
    {
        var totalParts = new Dictionary<string, int>();

        if (orders.Count == 0)
        {
            CommandLineError.Display("No orders to process");
            return;
        }

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

    internal void Instructions(List<Order> orders)
    {

        if (orders.Count == 0)
        {
            CommandLineError.Display("No orders to process");
            return;
        }

        orders.ForEach(order =>
        {
            var robotType = order.RobotType;
            var quantity = order.Quantity;

            foreach (var index in Enumerable.Range(1, quantity + 1))
            {
                var robot = new Robot(robotType);
                DisplayUtils.PrintStep($"{ProductionStep.Producing.Stringify()}", robotType.Stringify() + " :");

                var compatibleCore = robotType.GetCompatibleCore();
                DisplayUtils.PrintStep(ProductionStep.GetOutStock.Stringify(),
                    [compatibleCore.Quantity.ToString(), compatibleCore.Type.Stringify()]);
                var core = GetOutStock(compatibleCore.Type, inventoryService.PopCoreByType);
                var supportedSystem = core.Type.GetSupportedSystems().First();
                DisplayUtils.PrintStep(ProductionStep.Install.Stringify(), [supportedSystem.Stringify(), core.ToString()]);
                Install(supportedSystem, core);
                robot.SetCore(core);

                var compatibleGenerator = robotType.GetCompatibleGenerator();
                DisplayUtils.PrintStep(ProductionStep.GetOutStock.Stringify(), [compatibleGenerator.Quantity.ToString(), compatibleGenerator.Type.Stringify()]);
                var generator = GetOutStock(compatibleGenerator.Type, inventoryService.PopGeneratorByType);

                var name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                DisplayUtils.PrintStep(ProductionStep.Assemble.Stringify(), [name, core.ToString(), generator.ToString()]);
                var assembly = Assemble(name, generator, core);
                inventoryService.AddAssembly(assembly);
                robot.SetGenerator(generator);

                var compatibleArms = robotType.GetCompatibleArms();
                DisplayUtils.PrintStep(ProductionStep.GetOutStock.Stringify(), [compatibleArms.Quantity.ToString(), compatibleArms.Type.Stringify()]);
                var arms = GetOutStock(compatibleArms.Type, inventoryService.PopArmsByType);

                name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                DisplayUtils.PrintStep(ProductionStep.Assemble.Stringify(), [name, assembly.Name, arms.ToString()]);
                assembly = Assemble(name, assembly, arms);
                inventoryService.AddAssembly(assembly);
                robot.SetArms(arms);

                var compatibleLegs = robotType.GetCompatibleLegs();
                DisplayUtils.PrintStep(ProductionStep.GetOutStock.Stringify(), [compatibleLegs.Quantity.ToString(), compatibleLegs.Type.Stringify()]);
                var legs = GetOutStock(compatibleLegs.Type, inventoryService.PopLegsByType);

                name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                DisplayUtils.PrintStep(ProductionStep.Assemble.Stringify(), [name, assembly.Name, legs.ToString()]);
                assembly = Assemble(name, assembly, legs);
                inventoryService.AddAssembly(assembly);
                robot.SetLegs(legs);

                DisplayUtils.PrintStep(ProductionStep.Finished.Stringify(), robotType.Stringify());

                Console.WriteLine();
            }
        });
    }

    internal bool Verify(List<Order> orders)
    {
        var totalParts = new Dictionary<Enum, int>();

        if (orders.Count == 0)
        {
            CommandLineError.Display("No orders to process");
            return false;
        }

        foreach (var order in orders)
        {
            var robotType = order.RobotType;
            var robotQuantity = order.Quantity;

            var core = robotType.GetCompatibleCore();
            AddPart(core.Type, robotQuantity * core.Quantity, totalParts);

            var generator = robotType.GetCompatibleGenerator();
            AddPart(generator.Type, robotQuantity * generator.Quantity, totalParts);

            var arms = robotType.GetCompatibleArms();
            AddPart(arms.Type, robotQuantity * arms.Quantity, totalParts);

            var legs = robotType.GetCompatibleLegs();
            AddPart(legs.Type, robotQuantity * legs.Quantity, totalParts);
        }

        foreach (var (key, required) in totalParts)
        {
            var available = key switch
            {
                CoreType coreType => inventoryService.GetCoresByType(coreType).Count,
                GeneratorType generatorType => inventoryService.GetGeneratorsByType(generatorType).Count,
                ArmsType armType => inventoryService.GetArmsByType(armType).Count,
                LegsType legType => inventoryService.GetLegsByType(legType).Count,
                _ => throw new InvalidOperationException($"Unsupported part type: {key}")
            };

            if (available < required){
                DisplayUtils.PrintStatus(Status.Unavailable.Stringify());
                return false;
            }
        }
        DisplayUtils.PrintStatus(Status.Available.Stringify());
        return true;
    }

    internal void Produce(List<Order> orders)
    {
        if (!Verify(orders))
        {
            CommandLineError.Display("Stock is not enough for production");
            return;
        }

        orders.ForEach(order =>
        {
            var robotType = order.RobotType;
            var quantity = order.Quantity;

            foreach (var index in Enumerable.Range(1, quantity + 1))
            {
                var robot = new Robot(robotType);

                var compatibleCore = robotType.GetCompatibleCore();
                var core = GetOutStock(compatibleCore.Type, inventoryService.PopCoreByType);
                var supportedSystem = core.Type.GetSupportedSystems().First();
                Install(supportedSystem, core);
                robot.SetCore(core);

                var compatibleGenerator = robotType.GetCompatibleGenerator();
                var generator = GetOutStock(compatibleGenerator.Type, inventoryService.PopGeneratorByType);

                var name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                var assembly = Assemble(name, generator, core);
                inventoryService.AddAssembly(assembly);
                robot.SetGenerator(generator);

                var compatibleArms = robotType.GetCompatibleArms();
                var arms = GetOutStock(compatibleArms.Type, inventoryService.PopArmsByType);

                name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                assembly = Assemble(name, assembly, arms);
                inventoryService.AddAssembly(assembly);
                robot.SetArms(arms);

                var compatibleLegs = robotType.GetCompatibleLegs();
                var legs = GetOutStock(compatibleLegs.Type, inventoryService.PopLegsByType);

                name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                assembly = Assemble(name, assembly, legs);
                inventoryService.AddAssembly(assembly);
                robot.SetLegs(legs);

                inventoryService.AddRobot(robot);
            }
        });

        DisplayUtils.PrintStatus(Status.StockUpdated.Stringify());
    }

    private void AddPart(string key, int value, Dictionary<string, int> map)
    {
        if(!map.TryAdd(key, value))
        {
            map[key] += value;
        }
    }

    private void AddPart(Enum key, int value, Dictionary<Enum, int> map)
    {
        if (!map.TryAdd(key, value))
        {
            map[key] += value;
        }
    }


    private void Install(CoreSystem coreSystem, Core core)
    {
        try
        {
            core.InstallProgram(coreSystem);
        }
        catch (InvalidOperationException e)
        {
            CommandLineError.Display(e.Message);
        }
    }

    private TItem GetOutStock<TType, TItem>(TType type, Func<TType, TItem> getItemOutByType)
        where TItem: IPart<TType>
    {

        return getItemOutByType(type);
    }

    private Assembly Assemble<TItem1, TItem2>(string name, TItem1 part1, TItem2 part2)
    {
        return new Assembly(name, part1, part2);
    }

}