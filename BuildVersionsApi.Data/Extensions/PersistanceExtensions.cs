namespace BuildVersionsApi.Data.Extensions;

using BuildVersionsApi.Data.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class PersistanceExtensions
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, string connectionString)
    {
        ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

        _ = services.AddDbContext<BuildVersionsDbContext>(options => options.UseMySql(connectionString, serverVersion));
        _ = services.AddTransient<IPersistanceService, PersistanceService>();

        return services;
    }

    public static IHost ConfigurePersistance(this IHost app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        _ = scope.ServiceProvider.GetRequiredService<BuildVersionsDbContext>().EnsureDbExists();

        return app;
    }
}
