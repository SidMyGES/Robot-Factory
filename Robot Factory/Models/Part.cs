
namespace Robot_Factory.Models
{
    internal abstract class Part(string name)
    {
        protected string Name { get; private set; } = name;
    }
}
