using Robot_Factory.Models.Types;
using Robot_Factory.Validation;

namespace Robot_Factory.Validation.Strategies;

internal class DomesticStrategy : ICompatibilityStrategy
{
    public static DomesticStrategy Instance { get; } = new DomesticStrategy();

    private DomesticStrategy() { }

    public bool IsCompatiblePart(PartCategory partCategory)
        => partCategory is PartCategory.Domestic or PartCategory.Industrial or PartCategory.General;

    public bool IsCompatibleSystem(Data.System system)
        => IsCompatiblePart(system.Category);
}