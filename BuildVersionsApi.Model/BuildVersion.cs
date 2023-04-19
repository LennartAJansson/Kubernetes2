namespace BuildVersionsApi.Model;

public sealed class BuildVersion : BaseLoggedEntity
{
    //https://devopsnet.com/2011/06/09/build-versioning-strategy/
    public int Id { get; set; }

    public string ProjectName { get; set; } = "";

    public int Major
    {
        get => major;
        set
        {
            if (value > major)
            {
                major = value;
                Minor = 0;
                Build = 0;
                Revision = 0;
            }
        }
    }
    private int major;
    public int Minor
    {
        get => minor;
        set
        {
            if (value > minor)
            {
                minor = value;
                Build = 0;
                Revision = 0;
            }
        }
    }
    private int minor;
    public int Build
    {
        get => build;
        set
        {
            if (value > build)
            {
                build = value;
                Revision = 0;
            }
        }
    }
    private int build;

    public int Revision { get; set; }

    public string SemanticVersionText { get; set; } = "";

    public Version Version => new(Major, Minor, Build, Revision);
    public string Release => $"{Major}.{Minor}";
    public string SemanticVersion => SemanticVersionText == string.Empty ? $"{Major}.{Minor}.{Build}" : $"{Major}.{Minor}.{Build}-{SemanticVersionText}.{Revision}";
    public string SemanticRelease => SemanticVersionText == string.Empty ? $"{Major}.{Minor}" : $"{Major}.{Minor}-{SemanticVersionText}.{Build}.{Revision}";
}

