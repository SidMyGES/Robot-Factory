using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Robot(RobotType type): IPart<RobotType>
{
    public RobotType Type { get; private set; } = type;
    public (Core core, Generator generator, Arms arms, Legs legs) Parts { get; private set; }

    public override string ToString()
    {
        return Type.Stringify();
    }


    public void SetCore(Core core)
    {
        if (Parts.core != null)
        {
            throw new InvalidOperationException("Core already set");
        }

        Parts = Parts with { core = core };
    }

    public void SetGenerator(Generator generator)
    {
        if (Parts.generator != null)
        {
            throw new InvalidOperationException("Generator already set");
        }
        Parts = Parts with { generator = generator };
    }

    public void SetArms(Arms arms)
    {
        if (Parts.arms != null)
        {
            throw new InvalidOperationException("Arms already set");
        }
        Parts = Parts with { arms = arms };
    }

    public void SetLegs(Legs legs)
    {
        if (Parts.legs != null)
        {
            throw new InvalidOperationException("Legs already set");
        }
        Parts = Parts with { legs = legs };
    }

    public bool IsComplete()
    {
        return Parts is { core: not null, generator: not null, arms: not null, legs: not null };
    }

}