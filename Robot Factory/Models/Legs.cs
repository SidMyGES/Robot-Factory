using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Legs(LegType type)
{
    public LegType Type { get; private set; } = type;

    public override string ToString()
    {
        return Type.Stringify();
    }
}