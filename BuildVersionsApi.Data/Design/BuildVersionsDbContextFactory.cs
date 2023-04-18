namespace BuildVersionsApi.Data.Design;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BuildVersionsDbContextFactory : IDesignTimeDbContextFactory<BuildVersionsDbContext>
{
    //TODO Implement User Secrets in DesignTime
    private static readonly string connectionString = "Server=localhost;Port=3306;User=root;Password=password;Database=BuildVersionsNew";

    public BuildVersionsDbContext CreateDbContext(string[] args)
    {
        ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
        DbContextOptionsBuilder<BuildVersionsDbContext> optionsBuilder = new();
        _ = optionsBuilder.UseMySql(connectionString, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        return new BuildVersionsDbContext(optionsBuilder.Options);
    }
}

/*
Add-Migration -Name Initial -Context BuildVersionsDb -Project BuildVersionsApi.Data -StartupProject BuildVersionsApi.Data 

Add-Migration [-Name] <String> [-OutputDir <String>] [-Context <String>] [-Project <String>] [-StartupProject <String>] [-Namespace <String>] [-Args 
    <String>] [<CommonParameters>]
*/