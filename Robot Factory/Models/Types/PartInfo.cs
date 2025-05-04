namespace Robot_Factory.Models.Types;

internal record PartInfo<T>(T Type, int Quantity) where T : Enum;