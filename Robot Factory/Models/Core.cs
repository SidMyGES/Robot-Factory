using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Core(CoreType type) : IPart<CoreType>
{
    public CoreType Type { get; private set; } = type;
    public CoreSystem System { get; private set; }
    public bool HasProgramInstalled { get; private set; }

    public override string ToString()
    {
        return Type.Stringify();
    }

    public void InstallProgram(CoreSystem system)
    {
        if (HasProgramInstalled)
        {
            throw new InvalidOperationException("Program already installed");
        }
        HasProgramInstalled = true;
        System = system;
    }
}