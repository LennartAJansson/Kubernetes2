namespace BuildVersionsApi.Mediator.Extensions;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

public static class MediatorExtensions
{
    public static IServiceCollection AddAppMediators(this IServiceCollection services)
    {
        _ = services.AddMediatR(typeof(MediatorExtensions).Assembly);

        return services;
    }
}
