
using System.Security.Cryptography.X509Certificates;
using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{
    internal class Core(CoreType type) : Part("Core")
    {
    private CoreType Type { get; } = type;
    }
}
