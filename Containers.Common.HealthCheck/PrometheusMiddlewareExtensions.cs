namespace Containers.Common.HealthCheck;

using Containers.Common.HealthCheck.Checks;
using Containers.Common.HealthCheck.Middleware;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Prometheus;

public static class PrometheusMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestMiddleware>();
    }

    public static IApplicationBuilder UseApplicationHealthChecks(this IApplicationBuilder builder)
    {
        builder = builder
            .UseHealthChecks(new PathString("/health"), new CustomHealthCheckOptions());

        return builder;
    }
}

public static class WebHealthCheckExtensions
{
    public static IServiceCollection AddICmpApplicationHealthChecks(this IServiceCollection services, HealthCheckParam[] checks)
    {
        IHealthChecksBuilder builder = services.AddHealthChecks();
        foreach (HealthCheckParam check in checks)
        {
            _ = builder.AddCheck(check.Title ?? string.Empty, new ICMPHealthCheck(check));
        }

        _ = builder.ForwardToPrometheus();


        return services;
    }

    public static IServiceCollection AddWebApplicationHealthChecks(this IServiceCollection services, HealthCheckParam[] checks)
    {
        IHealthChecksBuilder builder = services.AddHealthChecks();
        foreach (HealthCheckParam check in checks)
        {
            _ = builder.AddCheck(check.Title ?? string.Empty, new HttpHealthCheck(check));
        }

        _ = builder.ForwardToPrometheus();


        return services;
    }
}