using Robot_Factory.Models.Types;
using Robot_Factory.Validation;

namespace Robot_Factory.Models;

internal class Core(CoreType type, PartCategory category) : IPart<CoreType>
{
    public CoreType Type { get; } = type;
    public Data.System System { get; private set; }
    public PartCategory Category { get; private set; } = category;

    public bool HasProgramInstalled { get; private set; }

    public override string ToString()
    {
        return Type.Stringify();
    }


    public void InstallProgram(Data.System system)
    {
        if (HasProgramInstalled)
            throw new InvalidOperationException("A system is already installed.");

        var strategy = CompatibilityStrategyFactory.Get(Category);

        if (!strategy.IsCompatibleSystem(system))
            throw new InvalidOperationException($"System '{system.Type.Stringify()}' is not compatible with core category '{Category.Stringify()}'");

        System = system;
        HasProgramInstalled = true;
    }
} 