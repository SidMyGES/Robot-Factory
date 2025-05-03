namespace Robot_Factory.Utils;

public class ListUtils
{
    public static void PrintList<T>(List<T> list)
    {
        if (list.Count == 0) return;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(list.Count);
        Console.ResetColor();
        Console.WriteLine(" " + list.First());
    }
}