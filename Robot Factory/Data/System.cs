using Robot_Factory.Models.Types;

namespace Robot_Factory.Data;

internal class System
{
    public CoreSystem Type { get; }
    public PartCategory Category { get; }

    private static readonly Dictionary<CoreSystem, System> Instances = new();

    private System(CoreSystem type, PartCategory category)
    {
        Type = type;
        Category = category;
    }

    public static System Register(CoreSystem type, PartCategory category)
    {
        if (Instances.ContainsKey(type))
            throw new InvalidOperationException($"System {type} is already registered.");

        var system = new System(type, category);
        Instances[type] = system;
        return system;
    }

    public static System Get(CoreSystem type)
    {
        if (!Instances.TryGetValue(type, out var system))
            throw new KeyNotFoundException($"System {type} has not been registered.");
        return system;
    }

    public static bool Exists(CoreSystem type) => Instances.ContainsKey(type);

    public override string ToString() => Type.Stringify();

    public static IEnumerable<System> GetAll()
    {
        return Instances.Values;
    }
}  