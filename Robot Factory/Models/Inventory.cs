
using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{
    internal class Inventory
    {
        private readonly Dictionary<CoreType, List<Core>> _coreInventory = new();
        private readonly Dictionary<ArmsType, List<Arms>> _armsInventory = new();
        private readonly Dictionary<GeneratorType, List<Generator>> _generatorInventory = new();
        private readonly Dictionary<LegsType, List<Legs>> _legsInventory = new();
        private readonly Dictionary<RobotType, List<Robot>> _robotInventory = new();

        internal List<Core> GetCoresByType(CoreType type)
        {
            return _coreInventory[type];
        }

        internal List<Arms> GetArmsByType(ArmsType type)
        {
            return _armsInventory[type];
        }

        internal List<Legs> GetLegsByType(LegsType type)
        {
            return _legsInventory[type];
        }

        internal List<Generator> GetGeneratorsByType(GeneratorType type)
        {
            return _generatorInventory[type];
        }

        internal List<Robot> GetRobotsByType(RobotType type)
        {
            return _robotInventory[type];
        }
    }
}
