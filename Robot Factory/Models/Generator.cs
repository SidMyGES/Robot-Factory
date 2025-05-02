
using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{
    internal class Generator(GeneratorType type): Part("Generator")

    {
        private GeneratorType Type { get; } = type;
    }
}
