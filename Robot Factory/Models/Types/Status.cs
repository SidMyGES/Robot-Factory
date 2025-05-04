namespace Robot_Factory.Models.Types;

internal enum Status
{
    Available,
    Unavailable,
    StockUpdated
}

internal static class StatusExtension
{
    public static string Stringify(this Status status)
    {
        return status switch
        {
            Status.Available => "AVAILABLE",
            Status.Unavailable => "UNAVAILABLE",
            Status.StockUpdated => "STOCK_UPDATED",
            _ => throw new ArgumentOutOfRangeException($"Status {status} is not an available Status")
        };
    }

    public static Status ToStatus(this string status)
    {
        return status switch
        {
            "AVAILABLE" => Status.Available,
            "UNAVAILABLE" => Status.Unavailable,
            "STOCK_UPDATED" => Status.StockUpdated,
            _ => throw new ArgumentException($"{status} is not a valid type of status")
        };
    }
}