namespace Containers.Common.HealthCheck.Middleware;

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

using Prometheus;

public class PrometheusMiddleware
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
        _next = next;
    }

    // ILogger is injected into InvokeAsync
    public async Task InvokeAsync(HttpContext httpContext, ILogger<PrometheusMiddleware> logger)
    {
        //TODO Find a way to name the Gauge and the Counter for each implementation!!!
        string text = $"{httpContext.Request.Method} {httpContext.Request.GetDisplayUrl()}";
        logger.LogDebug("{timestamp} {info}", DateTime.Now, text);

        DateTime startDateTime = DateTime.Now;

        await _next(httpContext);

        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(httpContext.Request.GetDisplayUrl())
            .Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(httpContext.Request.GetDisplayUrl())
            .Inc();
    }
}
