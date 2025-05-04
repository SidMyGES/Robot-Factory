namespace Robot_Factory.Models;

internal interface IPart<out TType>
{
    public TType Type { get; }
}