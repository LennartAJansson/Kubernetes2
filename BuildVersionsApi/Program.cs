using BuildVersionsApi.Data.Extensions;
using BuildVersionsApi.Endpoints;
using BuildVersionsApi.Extensions;

using Containers.Common.HealthCheck;
using Containers.Common.HealthCheck.Checks;
using Containers.Common.Types;

using Microsoft.OpenApi.Models;

using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext());

ApplicationInfo appInfo = new(typeof(Program));
builder.Services.AddSingleton(appInfo);

IEnumerable<HealthCheckParam>? healthChecks = builder.Configuration.GetSection("HealthChecks").Get<IEnumerable<HealthCheckParam>>()
    ?? Enumerable.Empty<HealthCheckParam>();

_ = builder.Services.AddApplicationHealthChecks(builder.Configuration, (HealthCheckParam[])healthChecks);

builder.Services.AddApplication();
Console.WriteLine(builder.Configuration.GetConnectionString("BuildVersionsDb"));
builder.Services.AddPersistance(builder.Configuration.GetConnectionString("BuildVersionsDb")
    ?? throw new ArgumentException("Invalid or not found connectionstring"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "BuildVersionsApi",
    Version = $"v{appInfo.SemanticVersion}",
    Description = $"<i>Branch/Commit: {appInfo.Description}</i>"
}));

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()));

WebApplication app = builder.Build();

app.ConfigurePersistance();

app.UseCors();

app.UseRequestMiddleware("buildversionsapi_counter", "buildversionsapi_executiontime");
app.UseApplicationHealthChecks();
app.UsePrometheusMetrics();

if (app.Environment.IsDevelopment())
{
}
_ = app.UseSwagger();
_ = app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.MapBuildVersionEndpoints()
    .MapMetricsEndpoint()
    .MapPing();

app.Run();
