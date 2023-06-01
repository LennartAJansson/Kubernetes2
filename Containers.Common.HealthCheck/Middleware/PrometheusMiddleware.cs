namespace Containers.Common.HealthCheck.Middleware;

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

using Prometheus;

public sealed class RequestMiddleware
{
    private readonly RequestDelegate next;
    public static Gauge? RequestExecuteTime { get; set; }

    public static Counter? Counter { get; set; }

    public RequestMiddleware(RequestDelegate next, string counter, string gauge)
    {
        this.next = next;
        RequestExecuteTime = Metrics.CreateGauge(gauge, "Counts total execution time for handling requests",
            new GaugeConfiguration
            {
                LabelNames = new[] { "path" }
            });
        Counter = Metrics.CreateCounter(counter, "Counts total calls for handling requests",
            new CounterConfiguration
            {
                LabelNames = new[] { "path" }
            });
    }

    public async Task InvokeAsync(HttpContext httpContext, ILogger<RequestMiddleware> logger)
    {
        string text = $"{httpContext.Request.Method} {httpContext.Request.GetDisplayUrl()}";
        logger.LogDebug("{timestamp} {info}", DateTime.Now, text);

        //Stopwatch stopwatch = Stopwatch.StartNew();
        //await next(httpContext);
        //stopwatch.Stop();
        //RequestExecuteTime?.Labels(httpContext.Request.Path)
        //    .Set(stopwatch.ElapsedMilliseconds);


        using (RequestExecuteTime?.WithLabels(httpContext.Request.Path.ToString().ToLower()).NewTimer())
        {
            await next(httpContext);
        }

        Counter?.WithLabels(httpContext.Request.Path.ToString().ToLower())
            .Inc();
    }
}
