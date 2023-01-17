using BuildVersionsApi.App.Endpoints;
using BuildVersionsApi.App.Extensions;
using BuildVersionsApi.Data.Extensions;

using Containers.Common.HealthCheck;
using Containers.Common.HealthCheck.Checks;
using Containers.Common.Types;

using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ApplicationInfo appInfo = new(typeof(Program));
builder.Services.AddSingleton<ApplicationInfo>(appInfo);

_ = builder.Services.AddApplicationHealthChecks(builder.Configuration.GetSection("HealthChecks").Get<HealthCheckParam[]>()
        ?? throw new Exception("HealthCheckParameters is missing in configuration"));

builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration.GetConnectionString("BuildVersionsDb")
    ?? throw new ArgumentException("Invalid or not found connectionstring"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Configure the HTTP request pipeline.
app.ConfigurePersistance();
app.UseCors();

app.UseMyCustomMiddleware();
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
