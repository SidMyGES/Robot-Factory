namespace Robot_Factory.Models.Types;

internal struct Order(int quantity, RobotType robotType)
{
    public int Quantity { get; private set; } = quantity;
    public RobotType RobotType { get; private set; } = robotType;
}