namespace BuildVersionsApi.Extensions;

using Microsoft.Extensions.DependencyInjection;

public static class AppExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        _ = services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(AppExtensions).Assembly));

        return services;
    }
}
