using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Legs(LegsType type, PartCategory category): IPart<LegsType>
{
    public LegsType Type { get; private set; } = type;
    public PartCategory Category { get; private set; } = category;


    public override string ToString()
    {
        return Type.Stringify();
    }
}