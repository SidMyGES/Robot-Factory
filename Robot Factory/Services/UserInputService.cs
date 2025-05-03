
using Robot_Factory.Models;
using Robot_Factory.Models.Types;
using Robot_Factory.Utils;

namespace Robot_Factory.Services;

internal class UserInputService(InventoryService inventoryService)
{
    internal void Stocks()
    {
        EnumUtils.PrintItemsByType<RobotType, Robot> (
            inventoryService.GetRobotsByType);

        EnumUtils.PrintItemsByType<CoreType, Core>(
            inventoryService.GetCoresByType);

        EnumUtils.PrintItemsByType<ArmsType, Arms>(
            inventoryService.GetArmsByType);

        EnumUtils.PrintItemsByType<LegsType, Legs>(
            inventoryService.GetLegsByType);

        EnumUtils.PrintItemsByType<GeneratorType, Generator>(
            inventoryService.GetGeneratorsByType);
    }





}