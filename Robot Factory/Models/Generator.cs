using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Generator(GeneratorType type, PartCategory category): IPart<GeneratorType>
{
    public GeneratorType Type { get; private set; } = type;
    public PartCategory Category { get; private set; } = category;


    public override string ToString()
    {
        return Type.Stringify();
    }

}