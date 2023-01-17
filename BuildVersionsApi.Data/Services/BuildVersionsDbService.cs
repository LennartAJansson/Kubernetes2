﻿namespace BuildVersionsApi.Data.Services;

using BuildVersionsApi.Model;

using Containers.Common.Types;

using Microsoft.EntityFrameworkCore;

public class BuildVersionsDbService : IPersistanceService
{
    private readonly BuildVersionsDb context;

    public BuildVersionsDbService(BuildVersionsDb context)
    {
        this.context = context;
    }

    public async Task<BuildVersion?> AddProject(string projectName, int major = 0, int minor = 0, int build = 0, int revision = 0, string semVerText = "")
    {
        BuildVersion model = new()
        {
            ProjectName = projectName,
            Major = major,
            Minor = minor,
            Build = build,
            Revision = revision,
            SemanticVersionText = semVerText
        };
        _ = context.Add(model);
        _ = await context.SaveChangesAsync();

        return model;
    }

    public async Task<BuildVersion?> UpdateProject(int id, string projectName, int major = 0, int minor = 0, int build = 0, int revision = 0, string semVerText = "")
    {
        if (!await context.BuildVersions.AnyAsync(b => b.Id == id))
        {
            return null;
        }

        BuildVersion model = new()
        {
            Id = id,
            ProjectName = projectName,
            Major = major,
            Minor = minor,
            Build = build,
            Revision = revision,
            SemanticVersionText = semVerText
        };
        _ = context.Update(model);
        _ = await context.SaveChangesAsync();

        return model;
    }

    public async Task<BuildVersion?> IncreaseVersion(string projectName, VersionNumber number, int amount = 1)
    {
        if (!await context.BuildVersions.AnyAsync(b => b.ProjectName == projectName))
        {
            return null;
        }

        BuildVersion model = await context.BuildVersions.SingleAsync(b => b.ProjectName == projectName);

        switch (number)
        {
            case VersionNumber.Major:
                model.Major += amount;
                break;
            case VersionNumber.Minor:
                model.Minor += amount;
                break;
            case VersionNumber.Build:
                model.Build += amount;
                break;
            case VersionNumber.Revision:
                model.Revision += amount;
                break;
        }

        _ = await context.SaveChangesAsync();

        return model;
    }

    public async Task<BuildVersion?> GetById(int id)
    {
        BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b => b.Id == id);

        return model;
    }

    public async Task<BuildVersion?> GetByName(string projectName)
    {
        BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b => b.ProjectName == projectName);

        return model;
    }

    public Task<IEnumerable<BuildVersion>> GetAll()
    {
        return Task.FromResult(context.BuildVersions.AsEnumerable());
    }
}
