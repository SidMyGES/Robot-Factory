using Robot_Factory.Models.Types;
using Robot_Factory.Validation;

namespace Robot_Factory.Models;

internal class Robot(RobotType type) : IPart<RobotType>
{
    public RobotType Type { get; private set; } = type;
    public PartCategory Category { get; private set; } = type.GetRobotCategory();

    public (Core core, Generator generator, Arms arms, Legs legs) Parts { get; private set; }

    public override string ToString()
    {
        return Type.Stringify();
    }


    public void SetCore(Core core)
    {
        var strategy = CompatibilityStrategyFactory.Get(Category);

        if (!strategy.IsCompatiblePart(core.Category) || core.System.Type != CoreSystem.Sb1)
            throw new InvalidOperationException($"Core category {core.Category} is not compatible with robot type {Type}");

        if (Parts.core != null)
            throw new InvalidOperationException("Core already set");

        Parts = Parts with { core = core };
    }

    public void SetGenerator(Generator generator)
    {
        var strategy = CompatibilityStrategyFactory.Get(Category);

        if (!strategy.IsCompatiblePart(generator.Category))
            throw new InvalidOperationException($"Generator category {generator.Category} is not compatible with robot type {Type}");

        if (Parts.generator != null)
            throw new InvalidOperationException("Generator already set");

        Parts = Parts with { generator = generator };
    }
    
    public void SetArms(Arms arms)
    {
        var strategy = CompatibilityStrategyFactory.Get(Category);

        if (!strategy.IsCompatiblePart(arms.Category))
            throw new InvalidOperationException($"Arms category {arms.Category} is not compatible with robot type {Type}");

        if (Parts.arms != null)
            throw new InvalidOperationException("Arms already set");

        Parts = Parts with { arms = arms };
    }

    public void SetLegs(Legs legs)
    {
        var strategy = CompatibilityStrategyFactory.Get(Category);

        if (!strategy.IsCompatiblePart(legs.Category))
            throw new InvalidOperationException($"Legs category {legs.Category} is not compatible with robot type {Type}");

        if (Parts.legs != null)
            throw new InvalidOperationException("Legs already set");

        Parts = Parts with { legs = legs };
    }

    public bool IsComplete()
    {
        return Parts is { core: not null, generator: not null, arms: not null, legs: not null };
    }

}