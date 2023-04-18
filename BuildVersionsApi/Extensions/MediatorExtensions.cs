namespace BuildVersionsApi.Extensions;

using Microsoft.Extensions.DependencyInjection;

public static class MediatorExtensions
{
    public static IServiceCollection AddAppMediators(this IServiceCollection services)
    {
        _ = services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(MediatorExtensions).Assembly));

        return services;
    }
}
