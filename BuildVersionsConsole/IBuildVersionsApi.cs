namespace BuildVersionsConsole;

using BuildVersionsApi.Contract;

using Containers.Common.Types;

using Refit;

public interface IBuildVersionsApi
{
    [Post("/buildversions/CreateProject")]
    Task<ResponseObject> CreateProject(AddProjectRequest request);

    [Put("/buildversions/UpdateProject")]
    Task<ResponseObject> UpdateProject(UpdateProjectRequest request);

    [Get("/buildversions/NewMajorVersion/{name}")]
    Task<ResponseObject> NewMajorVersion(string name);

    [Get("/buildversions/NewMinorVersion/{name}")]
    Task<ResponseObject> NewMinorVersion(string name);

    [Get("/buildversions/NewBuildVersion/{name}")]
    Task<ResponseObject> NewBuildVersion(string name);

    [Get("/buildversions/NewRevisionVersion/{name}")]
    Task<ResponseObject> NewRevisionVersion(string name);

    [Get("/buildversions/GetVersionById/{id}")]
    Task<ResponseObject> GetVersionById(int id);

    [Get("/buildversions/GetVersionByName/{name}")]
    Task<ResponseObject> GetVersionByName(string name);

    [Get("/buildversions/GetAll")]
    Task<ResponseObject> GetBuildVersions();
}
