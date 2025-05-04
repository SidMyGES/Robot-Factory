namespace Robot_Factory.Models.Types;

internal enum ProductionStep
{
    Producing,
    GetOutStock,
    Assemble,
    Install,
    Finished
}

internal static class ProductionStepExtensions
{
    public static string Stringify(this ProductionStep step)
    {
        return step switch
        {
            ProductionStep.Producing => "PRODUCING",
            ProductionStep.GetOutStock => "GET_OUT_STOCK",
            ProductionStep.Assemble => "ASSEMBLE",
            ProductionStep.Install => "INSTALL",
            ProductionStep.Finished => "FINISHED",
            _ => throw new ArgumentOutOfRangeException($"{step} is not implemented")
        };
    }

    public static ProductionStep ToProductionStep(this string step)
    {
        return step switch
        {
            "PRODUCING" => ProductionStep.Producing,
            "GET_OUT_STOCK" => ProductionStep.GetOutStock,
            "ASSEMBLE" => ProductionStep.Assemble,
            "INSTALL" => ProductionStep.Install,
            "FINISHED" => ProductionStep.Finished,
            _ => throw new ArgumentException($"{step} is not a valid type of production step")
        };
    }
}