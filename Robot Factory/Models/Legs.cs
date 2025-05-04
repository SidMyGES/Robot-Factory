using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Legs(LegsType type): IPart<LegsType>
{
    public LegsType Type { get; private set; } = type;

    public override string ToString()
    {
        return Type.Stringify();
    }
}