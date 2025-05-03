namespace Robot_Factory.Utils;

public class EnumUtils
{
    public static void PrintItemsByType<TEnum, TItem>(Func<TEnum, List<TItem>> getItemByType) where TEnum : struct, Enum
    {
        Enum.GetValues<TEnum>()
            .ToList()
            .ForEach(type =>
                ListUtils.PrintList(
                    getItemByType(type)));
    }
}