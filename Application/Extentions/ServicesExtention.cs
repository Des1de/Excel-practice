using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtention
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IExcelSerivce, ExcelService>();
        return services;
    }
}