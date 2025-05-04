using Robot_Factory.Models;

namespace Robot_Factory.Data;

internal class Inventory
{
    public List<Core> CoreInventory { get; private set; } = [];
    public List<Arms> ArmInventory { get; private set; } = [];
    public List<Generator> GeneratorInventory { get; private set;} = [];
    public List<Legs> LegInventory { get; private set;} = [];
    public List<Robot> RobotInventory { get; private set;} = [];
    public List<Assembly> AssemblyInventory { get; private set; } = [];

}