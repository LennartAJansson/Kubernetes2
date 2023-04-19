namespace BuildVersionsApi.Contracts;
public sealed record BuildVersionResponse(int Id, string ProjectName, int Major, int Minor, int Build, int Revision,
    string SemanticVersionText, string Version, string Release, string SemanticVersion, string SemanticRelease)
{
    public static BuildVersionResponse Instance(int id, string projectName, int major, int minor, int build, int revision,
        string semanticVersionText, string version, string release, string semanticVersion, string semanticRelease)
    {
        return new(id, projectName, major, minor, build, revision, semanticVersionText, version, release, semanticVersion, semanticRelease);
    }
};

public sealed record MediatorResponse(int Status, string Message, object? Data)
{
    public static MediatorResponse Instance(int status, string message, object? data)
    {
        return new(status, message, data);
    }
}