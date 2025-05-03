using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Arms (ArmsType type) : Part("Arms")
{
    public ArmsType Type { get; private set; } = type;

    public override string ToString()
    {
        return Name + "_" + Type.Stringify();
    }
}