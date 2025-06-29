using Robot_Factory.Models.Types;

namespace Robot_Factory.Validation.Strategies;

internal class MilitaryStrategy : ICompatibilityStrategy
{
    public static MilitaryStrategy Instance { get; } = new();

    private MilitaryStrategy() { }

    public bool IsCompatiblePart(PartCategory category) =>
        category is PartCategory.Military or PartCategory.Industrial;

    public bool IsCompatibleSystem(Data.System system) =>
        system.Category is PartCategory.Military or PartCategory.General;
}