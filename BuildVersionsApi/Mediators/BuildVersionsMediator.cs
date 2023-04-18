namespace BuildVersionsApi.Mediators;

using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Contracts;
using BuildVersionsApi.Data.Services;
using BuildVersionsApi.Mappings;
using BuildVersionsApi.Model;

using MediatR;

public class BuildVersionsMediator :
    IRequestHandler<AddProjectRequest, MediatorResponse>,
    IRequestHandler<UpdateProjectRequest, MediatorResponse>,
    IRequestHandler<IncreaseRequest, MediatorResponse>,
    IRequestHandler<GetBuildVersionByNameRequest, MediatorResponse>,
    IRequestHandler<GetBuildVersionByIdRequest, MediatorResponse>,
    IRequestHandler<GetAllBuildVersions, MediatorResponse>
{
    private readonly IPersistanceService service;

    public BuildVersionsMediator(IPersistanceService service)
    {
        this.service = service;
    }
    public async Task<MediatorResponse> Handle(AddProjectRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.AddProject(request.ProjectName, request.Major, request.Minor, request.Build, request.Revision, request.SemanticVersionText);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.UpdateProject(request.Id, request.ProjectName, request.Major, request.Minor, request.Build, request.Revision, request.SemanticVersionText);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(IncreaseRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.IncreaseVersion(request.ProjectName, request.Number);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(GetBuildVersionByNameRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.GetByName(request.ProjectName);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(GetBuildVersionByIdRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.GetById(request.Id);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(GetAllBuildVersions request, CancellationToken cancellationToken)
    {
        IEnumerable<BuildVersion> models = await service.GetAll();

        return !models.Any()
            ? MediatorResponse.Instance(404, "Not found", Enumerable.Empty<BuildVersionResponse>())
            : MediatorResponse.Instance(200, "Ok", models.Select(m => m.ToBuildVersionResponse()));
    }
}

