using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Generator(GeneratorType type): Part("Generator")
{
    public GeneratorType Type { get; private set; } = type;

    public override string ToString()
    {
        return Name + "_" + Type.Stringify();
    }
}