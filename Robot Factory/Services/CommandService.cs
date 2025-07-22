
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

            var core = robotType.GetCompatibleCoreType();
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
        var produce = false;

        if (orders.Count == 0)
        {
            CommandLineError.Display("No orders to process");
            return;
        }

        orders.ForEach(order =>
        {
            var robotType = order.RobotType;
            var quantity = order.Quantity;

            try
            {
                foreach (var index in Enumerable.Range(1, quantity))
                {
                    var robot = new Robot(robotType);
                    DisplayUtils.PrintStep($"{ProductionStep.Producing.Stringify()}", robotType.Stringify() + " :");

                    var compatibleCoreType = robotType.GetCompatibleCoreType();
                    DisplayUtils.PrintStep(ProductionStep.GetOutStock.Stringify(),
                        [compatibleCoreType.Quantity.ToString(), compatibleCoreType.Type.Stringify()]);
                    var core = FindCompatibleCore(robotType, compatibleCoreType.Type, produce);

                    var supportedSystemTypes = core.Type.GetSupportedSystemTypes();

                    var compatibleSystems = robotType.GetCompatibleSystems();

                    var chosenSystem = supportedSystemTypes
                        .Select(Data.System.Get)
                        .FirstOrDefault(system => compatibleSystems.Contains(system));

                    if (chosenSystem == null)
                    {
                        CommandLineError.Display(
                            $"No compatible system found for robot {robotType.Stringify()} and core {core.Type.Stringify()}");
                        return;
                    }

                    DisplayUtils.PrintStep(ProductionStep.Install.Stringify(),
                        [chosenSystem.ToString(), core.ToString()]);
                    Install(chosenSystem, core, produce);
                    robot.SetCore(core);

                    var compatibleGeneratorType = robotType.GetCompatibleGenerator();
                    DisplayUtils.PrintStep(ProductionStep.GetOutStock.Stringify(),
                        [compatibleGeneratorType.Quantity.ToString(), compatibleGeneratorType.Type.Stringify()]);
                    var generator = FindCompatibleGenerator(robotType, compatibleGeneratorType.Type, produce);
                    robot.SetGenerator(generator);

                    var name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                    DisplayUtils.PrintStep(ProductionStep.Assemble.Stringify(),
                        [name, core.ToString(), generator.ToString()]);
                    var assembly = Assemble(name, generator, core);
                    inventoryService.AddAssembly(assembly);

                    var compatibleArmsType = robotType.GetCompatibleArms();
                    DisplayUtils.PrintStep(ProductionStep.GetOutStock.Stringify(),
                        [compatibleArmsType.Quantity.ToString(), compatibleArmsType.Type.Stringify()]);
                    var arms = FindCompatibleArms(robotType, compatibleArmsType.Type, produce);
                    robot.SetArms(arms);

                    name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                    DisplayUtils.PrintStep(ProductionStep.Assemble.Stringify(), [name, assembly.Name, arms.ToString()]);
                    assembly = Assemble(name, assembly, arms);
                    inventoryService.AddAssembly(assembly);

                    var compatibleLegsType = robotType.GetCompatibleLegs();
                    DisplayUtils.PrintStep(ProductionStep.GetOutStock.Stringify(),
                        [compatibleLegsType.Quantity.ToString(), compatibleLegsType.Type.Stringify()]);
                    var legs = FindCompatibleLegs(robotType, compatibleLegsType.Type, produce);
                    robot.SetLegs(legs);

                    name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                    DisplayUtils.PrintStep(ProductionStep.Assemble.Stringify(), [name, assembly.Name, legs.ToString()]);
                    assembly = Assemble(name, assembly, legs);
                    inventoryService.AddAssembly(assembly);

                    DisplayUtils.PrintStep(ProductionStep.Finished.Stringify(), robotType.Stringify());
                    Console.WriteLine();
                    inventoryService.ClearAssemblies();
                }
            }
            catch (Exception e)
            {
                CommandLineError.Display(e.Message);
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

            var core = robotType.GetCompatibleCoreType();
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
        var produce = true;
        if (!Verify(orders))
        {
            CommandLineError.Display("Stock is not enough for production");
            return;
        }

        orders.ForEach(order =>
        {
            var robotType = order.RobotType;
            var quantity = order.Quantity;
            try 
            {
                foreach (var index in Enumerable.Range(1, quantity))
                {
                    var robot = new Robot(robotType);

                    var compatibleCoreType = robotType.GetCompatibleCoreType();
                    var core = FindCompatibleCore(robotType, compatibleCoreType.Type, produce);

                    var supportedSystemTypes = core.Type.GetSupportedSystemTypes();

                    var compatibleSystems = robotType.GetCompatibleSystems();

                    var chosenSystem = supportedSystemTypes
                        .Select(Data.System.Get)
                        .FirstOrDefault(system => compatibleSystems.Contains(system));

                    if (chosenSystem == null)
                    {
                        CommandLineError.Display($"No compatible system found for robot {robotType.Stringify()} and core {core.Type.Stringify()}");
                        return;
                    }

                    DisplayUtils.PrintStep(ProductionStep.Install.Stringify(), [chosenSystem.ToString(), core.ToString()]);
                    Install(chosenSystem, core, produce);
                    robot.SetCore(core);


                    var compatibleGeneratorType = robotType.GetCompatibleGenerator();
                    var generator = FindCompatibleGenerator(robotType, compatibleGeneratorType.Type, produce);
                    robot.SetGenerator(generator);

                    var name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                    var assembly = Assemble(name, generator, core);
                    inventoryService.AddAssembly(assembly);

                    var compatibleArmsType = robotType.GetCompatibleArms();
                    var arms = FindCompatibleArms(robotType, compatibleArmsType.Type, produce);
                    robot.SetArms(arms);

                    name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                    assembly = Assemble(name, assembly, arms);
                    inventoryService.AddAssembly(assembly);

                    var compatibleLegsType = robotType.GetCompatibleLegs();
                    var legs = FindCompatibleLegs(robotType, compatibleLegsType.Type, produce);
                    robot.SetLegs(legs);

                    name = "TMP" + (inventoryService.GetAssemblies().Count + 1);
                    assembly = Assemble(name, assembly, legs);
                    inventoryService.AddAssembly(assembly);

                    inventoryService.AddRobot(robot);
                    inventoryService.ClearAssemblies();
                }

            }
            catch (Exception e)
            {
                CommandLineError.Display(e.Message);
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


    private void Install(Data.System system, Core core, bool produce)
    {
        try
        {
            core.InstallProgram(system);
        }
        catch (InvalidOperationException e)
        {
            if(produce)
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

    private Core FindCompatibleCore(RobotType robotType, CoreType type, bool produce)
    {
        var categories = robotType.GetCompatibleCoreCategories();

        foreach (var category in categories)
        {
            var item = inventoryService.GetCoresByType(type).FirstOrDefault(g => g.Category == category);
            if (item != null)
                return produce == true ? inventoryService.PopCoreByTypeAndCategory(type, category) : item;
        }

        throw new InvalidOperationException($"No compatible Core available for {robotType}");
    }
    private Generator FindCompatibleGenerator(RobotType robotType, GeneratorType type, bool produce)
    {
        var categories = robotType.GetCompatibleCoreCategories();
        foreach (var category in categories)
        {
            var item = inventoryService.GetGeneratorsByType(type).FirstOrDefault(g => g.Category == category);
            if (item != null) 
                return produce == true ? inventoryService.PopGeneratorByTypeAndCategory(type, category) : item;
        }

        throw new InvalidOperationException("No compatible generator found in stock.");
    }

    private Arms FindCompatibleArms(RobotType robotType, ArmsType type, bool produce)
    {
        var categories = robotType.GetCompatibleCoreCategories();
        foreach (var category in categories)
        {
            var item = inventoryService.GetArmsByType(type).FirstOrDefault(g => g.Category == category);
            if (item != null) 
                return produce == true ? inventoryService.PopArmsByTypeAndCategory(type, category) : item;
        }

        throw new InvalidOperationException("No compatible generator found in stock.");
    }

    private Legs FindCompatibleLegs(RobotType robotType, LegsType type, bool produce)
    {
        var categories = robotType.GetCompatibleCoreCategories();
        foreach (var category in categories)
        {
            var item = inventoryService.GetLegsByType(type).FirstOrDefault(g => g.Category == category);
            if (item != null) 
                return produce == true ? inventoryService.PopLegsByTypeAndCategory(type, category) : item;
        }

        throw new InvalidOperationException("No compatible generator found in stock.");
    }

}