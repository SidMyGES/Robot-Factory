using Robot_Factory.Models.Types;

namespace Robot_Factory.Models;

internal class Robot(RobotType type)
{
    public RobotType Type { get; private set; } = type;
    //public Core Core { get; set; }
    //public Generator Generator { get; set; }
    //public Arms Arms { get; set; }
    //public Legs Legs { get; set; }

    public override string ToString()
    {
        return Type.Stringify();
    }
}