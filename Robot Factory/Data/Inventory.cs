using Robot_Factory.Models;

namespace Robot_Factory.Data;

internal class Inventory
{
    public List<Core> CoreInventory { get; private set; } = [];
    public List<Arms> ArmsInventory { get; private set; } = [];
    public List<Generator> GeneratorInventory { get; private set;} = [];
    public List<Legs> LegsInventory { get; private set;} = [];
    public List<Robot> RobotInventory { get; private set;} = [];


}