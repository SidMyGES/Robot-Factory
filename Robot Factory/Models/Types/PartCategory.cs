namespace Robot_Factory.Models.Types;

internal enum PartCategory
{
    General,     
    Domestic,    
    Industrial,  
    Military     
}

internal static class PartCategoryExtensions
{
    public static string Stringify(this PartCategory category)
    {
        return category switch
        {
            PartCategory.Domestic => "D",
            PartCategory.Industrial => "I",
            PartCategory.Military => "M",
            _ => "G"
        };
    }

    public static PartCategory ToCategory(this string category)
    {
        return category switch
        {
            "D" => PartCategory.Domestic,
            "I" => PartCategory.Industrial,
            "M" => PartCategory.Military,
            _ => PartCategory.General
        };
    }
}