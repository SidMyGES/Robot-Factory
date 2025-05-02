using Robot_Factory.Models.Types;

namespace Robot_Factory.Models
{ 
    public class Robot(string name, CoreType model)
    {
        private string Name { get; } = name;
        private CoreType Model { get; } = model;
        //private Dictionary<>
    }
}
