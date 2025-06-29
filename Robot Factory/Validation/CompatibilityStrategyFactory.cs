using Robot_Factory.Models.Types;
using Robot_Factory.Validation.Strategies;

namespace Robot_Factory.Validation;

internal static class CompatibilityStrategyFactory
{
    public static ICompatibilityStrategy Get(PartCategory category) => category switch
    {
        PartCategory.Domestic => DomesticStrategy.Instance,
        PartCategory.Industrial => IndustrialStrategy.Instance,
        PartCategory.Military => MilitaryStrategy.Instance,
        _ => throw new ArgumentOutOfRangeException(nameof(category), $"Unsupported robot category: {category}")
    };
}

