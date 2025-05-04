using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Arms (ArmType type)
{
    public ArmType Type { get; private set; } = type;

    public override string ToString()
    {
        return Type.Stringify();
    }
}