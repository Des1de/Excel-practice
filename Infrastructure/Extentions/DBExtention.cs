using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DBExtention
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string? ConnectionString)
    {
        services.AddDbContext<ExcelDbContext>(
            options => 
            {
                options.UseNpgsql(ConnectionString,
                    b => b.MigrationsAssembly("Mvc"));  
            }
        );
        return services; 
    }
}