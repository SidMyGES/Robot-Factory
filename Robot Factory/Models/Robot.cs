using System.Reflection;
using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{ 
    public class Robot(RobotType type)
    {
        private RobotType Type { get; } = type;
        private Dictionary<Part, bool> _parts = new();

    }
}
