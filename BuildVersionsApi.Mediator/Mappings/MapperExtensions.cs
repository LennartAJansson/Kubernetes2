namespace BuildVersionsApi.Mediator.Mappings;
using BuildVersionsApi.Contract;
using BuildVersionsApi.Model;

public static class MapperExtensions
{
    public static BuildVersionResponse ToBuildVersionResponse(this BuildVersion buildVersion)
    {
        return new(buildVersion.Id, buildVersion.ProjectName, buildVersion.Major, buildVersion.Minor, buildVersion.Build, buildVersion.Revision,
            buildVersion.SemanticVersionText, buildVersion.Version.ToString(), buildVersion.Release, buildVersion.SemanticVersion, buildVersion.SemanticRelease);
    }
}
