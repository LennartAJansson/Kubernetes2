namespace BuildVersionsApi.Data.Services;

using BuildVersionsApi.Model;

using Containers.Common.Types;

public interface IPersistanceService
{
    Task<BuildVersion?> AddProject(string projectName, int major = 0, int minor = 0, int build = 0, int revision = 0, string semVerText = "");
    Task<BuildVersion?> UpdateProject(int id, string projectName, int major = 0, int minor = 0, int build = 0, int revision = 0, string semVerText = "");
    Task<BuildVersion?> IncreaseVersion(string projectName, VersionNumber number, int amount = 1);
    Task<BuildVersion?> GetById(int id);
    Task<BuildVersion?> GetByName(string projectName);
    Task<IEnumerable<BuildVersion>> GetAll();
}
