﻿namespace Containers.Common.HealthCheck;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using Prometheus;

public static class PrometheusMetricsExtensions
{
    public static IApplicationBuilder UsePrometheusMetrics(this IApplicationBuilder app)
    {
        app = app.UseHttpMetrics();

        return app;
    }
    public static IEndpointRouteBuilder MapMetricsEndpoint(this IEndpointRouteBuilder routes)
    {
        //Makes a route to https://localhost:7242/metrics that will respond with a prometheus record hidden from Swagger
        _ = routes.MapMetrics("/metrics");

        return routes;
    }

    public static IEndpointRouteBuilder MapPing(this IEndpointRouteBuilder routes)
    {
        _ = routes.Map("/ping", () => "pong")
        .WithDisplayName("Ping");

        return routes;
    }

}

