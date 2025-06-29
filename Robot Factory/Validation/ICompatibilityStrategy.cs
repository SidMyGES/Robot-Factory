using Robot_Factory.Models.Types;

namespace Robot_Factory.Validation;

internal interface ICompatibilityStrategy
{
    public bool IsCompatiblePart(PartCategory category);
    public bool IsCompatibleSystem(Data.System system);

}