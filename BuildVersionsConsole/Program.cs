using BuildVersionsApi.Model;

using BuildVersionsConsole;

using Containers.Common.Types;

using Refit;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(AddServices)
    .Build();

using (host)
{
    await host.StartAsync();

    using (IServiceScope scope = host.Services.CreateScope())
    {
        ILogger<Program> logger = scope
            .ServiceProvider
            .GetRequiredService<ILogger<Program>>();

        IBuildVersionsApi client = scope
            .ServiceProvider
            .GetRequiredService<IBuildVersionsApi>();

        await ReadAll(logger, client);

        await ReadOne(logger, client);
    }
    await host.WaitForShutdownAsync();
}

static void AddServices(HostBuilderContext context, IServiceCollection services)
{
    ApplicationInfo appInfo = new(typeof(Program));
    _ = services.AddSingleton<ApplicationInfo>(appInfo);

    _ = services.AddRefitClient<IBuildVersionsApi>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://buildversionsapi.local:8081"));
}

static async Task ReadAll(ILogger<Program> logger, IBuildVersionsApi client)
{
    logger.LogInformation("Trying to read all BuildVersions");
    ResponseObject response = await client
        .GetBuildVersions();

    IEnumerable<BuildVersion>? replies = response.TranslateResponse() as IEnumerable<BuildVersion>;
    if (replies is not null)
    {
        logger.LogInformation("Found {count} BuildVersion elements", replies.Count());

        foreach (BuildVersion? item in replies)
        {
            if (item is not null)
            {
                logger.LogInformation("{name} latest version is {version}", item.ProjectName, item.SemanticVersion);
            }
        }
    }
}

static async Task ReadOne(ILogger<Program> logger, IBuildVersionsApi client)
{
    logger.LogInformation("Trying to read one single BuildVersion by name");
    ResponseObject response = await client
        .GetVersionByName("buildversionsapi");
    BuildVersion? reply = response.TranslateResponse() as BuildVersion;
    if (reply is not null)
    {
        logger.LogInformation("{name} latest version is {version}", reply.ProjectName, reply.SemanticVersion);
    }
}