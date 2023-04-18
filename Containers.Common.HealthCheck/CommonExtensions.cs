namespace Containers.Common.HealthCheck;

using Containers.Common.HealthCheck.Checks;

using Microsoft.Extensions.DependencyInjection;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, HealthCheckParam[] checks)
    {
        IHealthChecksBuilder builder = services.AddHealthChecks();
        foreach (HealthCheckParam check in checks)
        {
            _ = builder.AddCheck(check.Title ?? string.Empty, new HttpHealthCheck(check));
        }

        return services;
    }
}
