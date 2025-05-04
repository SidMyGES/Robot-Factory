using Robot_Factory.Models.Types;

namespace Robot_Factory.Utils;

internal static class DisplayUtils
{

    public static void Print(string info, int quantity)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(quantity);
        Console.ResetColor();
        Console.WriteLine(" " + info);
    }

    public static void Print<T>(List<T> list)
    {
        if (list.Count == 0) return;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(list.Count);
        Console.ResetColor();
        Console.WriteLine(" " + list.First());
    }

    public static void Print<T>(List<T> list, int amount)
    {
        if (list.Count == 0) return;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(amount);
        Console.ResetColor();
        Console.WriteLine(" " + list.First());
    }

    public static void Print<TEnum, TItem>(Func<TEnum, List<TItem>> getItemByType) where TEnum : struct, Enum
    {
        Enum.GetValues<TEnum>()
            .ToList()
            .ForEach(type =>
                Print(getItemByType(type)));
    }
}