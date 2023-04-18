namespace BuildVersionsApi.Contracts;

using Containers.Common.Types;

using MediatR;

public record AddProjectRequest(string ProjectName, int Major, int Minor, int Build, int Revision, string SemanticVersionText) : IRequest<MediatorResponse>
{
    public static AddProjectRequest Instance(string projectName, int major, int minor, int build, int revision, string semanticVersionText)
    {
        return new(projectName, major, minor, build, revision, semanticVersionText);
    }
}
public record UpdateProjectRequest(int Id, string ProjectName, int Major, int Minor, int Build, int Revision, string SemanticVersionText) : IRequest<MediatorResponse>
{
    public static UpdateProjectRequest Instance(int id, string projectName, int major, int minor, int build, int revision, string semanticVersionText)
    {
        return new(id, projectName, major, minor, build, revision, semanticVersionText);
    }
}
public record IncreaseRequest(string ProjectName, VersionNumber Number) : IRequest<MediatorResponse>
{
    public static IncreaseRequest Instance(string projectName, VersionNumber number)
    {
        return new(projectName, number);
    }
};
public record GetBuildVersionByIdRequest(int Id) : IRequest<MediatorResponse>
{
    public static GetBuildVersionByIdRequest Instance(int id)
    {
        return new(id);
    }
};
public record GetBuildVersionByNameRequest(string ProjectName) : IRequest<MediatorResponse>
{
    public static GetBuildVersionByNameRequest Instance(string projectName)
    {
        return new(projectName);
    }
};
public record GetAllBuildVersions() : IRequest<MediatorResponse>
{
    public static GetAllBuildVersions Instance()
    {
        return new();
    }
};
