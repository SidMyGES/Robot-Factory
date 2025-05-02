
namespace Robot_Factory.Services
{
    internal class UserInputService
    {

        private readonly InventoryService _inventoryService = new();


        internal void Stocks()
        {
            PrintList(_inventoryService.GetRobots());
            PrintList(_inventoryService.GetCores());
            PrintList(_inventoryService.GetGenerators());
            PrintList(_inventoryService.GetArms());
            PrintList(_inventoryService.GetLegs());
        }


        private void PrintList<T>(List<T> list)
        {
            if (list.Count != 0)
                Console.WriteLine(list.Count + " " + list.);
        }
    }
}
