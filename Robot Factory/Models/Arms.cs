using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Arms(ArmsType type, PartCategory category):IPart<ArmsType>
{
    public ArmsType Type { get; private set; } = type;
    public PartCategory Category { get; private set; } = category;


    public override string ToString()
    {
        return Type.Stringify();
    }
}