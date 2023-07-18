namespace Containers.Common.HealthCheck;

using Containers.Common.HealthCheck.Checks;
using Containers.Common.HealthCheck.Middleware;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Prometheus;

public static class PrometheusMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder, string counter, string gauge)
        => builder.UseMiddleware<RequestMiddleware>(counter, gauge);

    public static IApplicationBuilder UseApplicationHealthChecks(this IApplicationBuilder builder)
    {
        builder = builder
            .UseHealthChecks(new PathString("/health"), new CustomHealthCheckOptions());

        return builder;
    }
}

public static class WebHealthCheckExtensions
{
    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, IConfiguration configuration, HealthCheckParam[] checks)
    {
        IHealthChecksBuilder builder = services.AddHealthChecks();
        foreach (HealthCheckParam check in checks)
        {
            if (check.Title is not null)
            {
                if (check.Title.ToLower().StartsWith("http_"))
                {
                    _ = builder.AddCheck(check.Title ?? string.Empty, new HttpHealthCheck(check));
                }
                else if (check.Title.ToLower().StartsWith("icmp_"))
                {
                    _ = builder.AddCheck(check.Title ?? string.Empty, new ICMPHealthCheck(check));
                }
                else if (check.Title.ToLower().StartsWith("db_"))
                {
                    check.Host = configuration.GetConnectionString(check.Host ?? string.Empty);
                    _ = builder.AddCheck(check.Title ?? string.Empty, new DbHealthCheck(check));
                }
            }
        }

        _ = builder.ForwardToPrometheus();


        return services;
    }
}