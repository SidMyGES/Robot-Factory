using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Generator(GeneratorType type): IPart<GeneratorType>
{
    public GeneratorType Type { get; private set; } = type;

    public override string ToString()
    {
        return Type.Stringify();
    }

}