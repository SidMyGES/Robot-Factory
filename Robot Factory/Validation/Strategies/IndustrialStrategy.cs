using Robot_Factory.Models.Types;
using Robot_Factory.Validation;

namespace Robot_Factory.Validation.Strategies
{
    internal class IndustrialStrategy : ICompatibilityStrategy
    {
        public static IndustrialStrategy Instance { get; } = new();

        private IndustrialStrategy() { }

        public bool IsCompatiblePart(PartCategory category) =>
            category is PartCategory.General or PartCategory.Industrial;

        public bool IsCompatibleSystem(Data.System system) =>
            IsCompatiblePart(system.Category);
    }
}