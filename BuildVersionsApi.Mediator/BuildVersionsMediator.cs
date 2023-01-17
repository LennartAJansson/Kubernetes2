namespace BuildVersionsApi.Mediator;

using BuildVersionsApi.Contract;
using BuildVersionsApi.Data.Services;
using BuildVersionsApi.Mediator.Mappings;
using BuildVersionsApi.Model;

using Containers.Common.Mediator;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

public class BuildVersionsMediator : MediatorBase,
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
            ? CreateResponse(404, "Not found", null)
            : CreateResponse(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.UpdateProject(request.Id, request.ProjectName, request.Major, request.Minor, request.Build, request.Revision, request.SemanticVersionText);

        return model is null
            ? CreateResponse(404, "Not found", null)
            : CreateResponse(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(IncreaseRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.IncreaseVersion(request.ProjectName, request.Number);

        return model is null
            ? CreateResponse(404, "Not found", null)
            : CreateResponse(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(GetBuildVersionByNameRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.GetByName(request.ProjectName);

        return model is null
            ? CreateResponse(404, "Not found", null)
            : CreateResponse(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(GetBuildVersionByIdRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.GetById(request.Id);

        return model is null
            ? CreateResponse(404, "Not found", null)
            : CreateResponse(200, "Ok", model.ToBuildVersionResponse());
    }

    public async Task<MediatorResponse> Handle(GetAllBuildVersions request, CancellationToken cancellationToken)
    {
        IEnumerable<BuildVersion> models = await service.GetAll();

        return models.Any()
            ? CreateResponse(200, "Ok", models.Select(m => m.ToBuildVersionResponse()))
            : CreateResponse(404, "Not found", Enumerable.Empty<BuildVersionResponse>());
    }
}

