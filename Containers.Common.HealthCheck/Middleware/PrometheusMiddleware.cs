namespace Containers.Common.HealthCheck.Middleware;

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

using Prometheus;

public sealed class PrometheusMiddleware
{
    private readonly RequestDelegate _next;
    protected static Gauge RequestExecuteTime { get; set; } = Metrics.CreateGauge("buildversionsapi_executiontime", "Counts total execution time for handling requests",
        new GaugeConfiguration
        {
            LabelNames = new[] { "path" }
        });

    protected static Counter Counter { get; set; } = Metrics.CreateCounter("buildversionsapi_counter", "Counts total calls for handling requests",
        new CounterConfiguration
        {
            LabelNames = new[] { "path" }
        });

    public PrometheusMiddleware(RequestDelegate next)
    {
        //TODO Find a way to name the Gauge and the Counter for each implementation!!!
        _next = next;
    }

    // ILogger is injected into InvokeAsync
    public async Task InvokeAsync(HttpContext httpContext, ILogger<PrometheusMiddleware> logger)
    {
        string text = $"{httpContext.Request.Method} {httpContext.Request.GetDisplayUrl()}";
        logger.LogDebug("{timestamp} {info}", DateTime.Now, text);

        Stopwatch stopwatch = Stopwatch.StartNew();

        await _next(httpContext);

        stopwatch.Stop();

        //RequestExecuteTime.Labels(httpContext.Request.GetDisplayUrl())
        RequestExecuteTime.Labels(httpContext.Request.Path)
            .Set(stopwatch.ElapsedMilliseconds);

        //Counter.Labels(httpContext.Request.GetDisplayUrl())
        Counter.Labels(httpContext.Request.Path)
            .Inc();
    }
}
