using System.Reflection;
using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{ 
    public class Robot(RobotType type)
    {
        public RobotType Type { get; private set; } = type;
        private Dictionary<Part, bool> _parts = new();

    }
}
