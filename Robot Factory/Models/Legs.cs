using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Legs(LegsType type): Part("Legs")
{
    public LegsType Type { get; private set; } = type;

    public override string ToString()
    {
        return Name + "_" + Type.Stringify();
    }
}