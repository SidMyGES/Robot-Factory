using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Core(CoreType type) : Part("Core")
{
    public CoreType Type { get; private set; } = type;

    public override string ToString()
    {
        return Name + "_" + Type.Stringify();
    }
}