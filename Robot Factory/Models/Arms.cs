using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Arms(ArmsType type):IPart<ArmsType>
{
    public ArmsType Type { get; private set; } = type;

    public override string ToString()
    {
        return Type.Stringify();
    }
}