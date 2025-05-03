
using Robot_Factory.Data;
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
        return _inventory.ArmsInventory.Where(arm => arm.Type == type).ToList();
    }

    public List<Legs> GetLegsByType(LegsType type)
    {
        return _inventory.LegsInventory.Where(leg => leg.Type == type).ToList();
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
        return _inventory.ArmsInventory;
    }

    public List<Legs> GetLegs()
    {
        return _inventory.LegsInventory;
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
        _inventory.ArmsInventory.Add(arms);
    }

    public void AddLegs(Legs legs)
    {
        _inventory.LegsInventory.Add(legs);
    }

    public void AddGenerator(Generator generator)
    {
        _inventory.GeneratorInventory.Add(generator);
    }

}