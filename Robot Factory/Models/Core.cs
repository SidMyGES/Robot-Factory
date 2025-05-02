
using System.Security.Cryptography.X509Certificates;

namespace Robot_Factory.Models
{
    internal class Core(string type) : Part("Core")
    {
    private string Type { get; } = type;
    }
}
