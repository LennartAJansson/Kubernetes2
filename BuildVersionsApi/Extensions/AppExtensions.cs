namespace BuildVersionsApi.App.Extensions;

using BuildVersionsApi.Mediator.Extensions;

using Microsoft.Extensions.DependencyInjection;

public static class AppExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        _ = services.AddAppMediators();

        return services;
    }
}
