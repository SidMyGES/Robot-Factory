
using Robot_Factory.Data;
using Robot_Factory.Errors;
using Robot_Factory.Models;
using Robot_Factory.Models.Types;

namespace Robot_Factory.Services;

internal class InventoryService
{
    private readonly Inventory _inventory = new();

    public List<Core> GetCoresByType(CoreType type)
    {
        return _inventory.CoreInventory.Where(core => core.Type == type).ToList();
    }

    public List<Arms> GetArmsByType(ArmsType type)
    {
        return _inventory.ArmInventory.Where(arm => arm.Type == type).ToList();
    }

    public List<Legs> GetLegsByType(LegsType type)
    {
        return _inventory.LegInventory.Where(leg => leg.Type == type).ToList();
    }

    public List<Generator> GetGeneratorsByType(GeneratorType type)
    {
        return _inventory.GeneratorInventory.Where(generator => generator.Type == type).ToList();
    }

    public List<Robot> GetRobotsByType(RobotType type)
    {
        return _inventory.RobotInventory.Where(robot => robot.Type == type).ToList();
    }

    public List<Core> GetCores()
    {
        return _inventory.CoreInventory;
    }

    public List<Arms> GetArms()
    {
        return _inventory.ArmInventory;
    }

    public List<Legs> GetLegs()
    {
        return _inventory.LegInventory;
    }

    public List<Generator> GetGenerators()
    {
        return _inventory.GeneratorInventory;
    }

    public List<Robot> GetRobots()
    {
        return _inventory.RobotInventory;
    }

    public void AddRobot(Robot robot)
    {
        _inventory.RobotInventory.Add(robot);
    }

    public void AddCore(Core core)
    {
        _inventory.CoreInventory.Add(core);
    }

    public void AddArms(Arms arms)
    {
        _inventory.ArmInventory.Add(arms);
    }

    public void AddLegs(Legs legs)
    {
        _inventory.LegInventory.Add(legs);
    }

    public void AddGenerator(Generator generator)
    {
        _inventory.GeneratorInventory.Add(generator);
    }

    public Robot? PopRobotByType(RobotType type)
    {
        var robot = _inventory.RobotInventory.FirstOrDefault(robot => robot.Type == type);
        if (robot != null)
        {
            _inventory.RobotInventory.Remove(robot);
        }

        return robot;
    }

    public Core PopCoreByType(CoreType type)
    {
        var core = _inventory.CoreInventory.FirstOrDefault(core => core.Type == type);
        if (core != null)
        {
            _inventory.CoreInventory.Remove(core);
        }
        else
        {
            CommandLineError.Display($"No core of type {type} available");
            throw new InvalidOperationException($"No core of type {type} available");
        }

        return core;
    }

    public Generator PopGeneratorByType(GeneratorType type)
    {
        var generator = _inventory.GeneratorInventory.FirstOrDefault(generator => generator.Type == type);
        if (generator != null)
        {
            _inventory.GeneratorInventory.Remove(generator);
        }
        else
        {
            CommandLineError.Display($"No generator of type {type} available");
            throw new InvalidOperationException($"No generator of type {type} available");
        }

        return generator;
    }

    public Arms PopArmsByType(ArmsType type)
    {
        var arm = _inventory.ArmInventory.FirstOrDefault(arm => arm.Type == type);
        if (arm != null)
        {
            _inventory.ArmInventory.Remove(arm);
        }
        else
        {
            CommandLineError.Display($"No arm of type {type} available");
            throw new InvalidOperationException($"No arm of type {type} available");
        }

        return arm;
    }

    public Legs PopLegsByType(LegsType type)
    {
        var leg = _inventory.LegInventory.FirstOrDefault(leg => leg.Type == type);
        if (leg != null)
        {
            _inventory.LegInventory.Remove(leg);
        }
        else
        {
            CommandLineError.Display($"No leg of type {type} available");
            throw new InvalidOperationException($"No leg of type {type} available");
        }

        return leg;
    }

    public void AddAssembly (Assembly assembly)
    {
        _inventory.AssemblyInventory.Add(assembly);
    }

    public Assembly PopAssembly(string name)
    {
        return _inventory.AssemblyInventory.FirstOrDefault(assembly => assembly.Name == name);
    }

    public List<Assembly> GetAssemblies()
    {
        return _inventory.AssemblyInventory;
    }
}