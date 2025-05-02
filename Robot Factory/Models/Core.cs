
using System.Security.Cryptography.X509Certificates;
using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{
    internal class Core(CoreType type) : Part("Core")
    {
    public CoreType Type { get; private set; } = type;
    }
}
