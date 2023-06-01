namespace BuildVersionsApi.Mediators;

using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Contracts;
using BuildVersionsApi.Data.Services;
using BuildVersionsApi.Mappings;
using BuildVersionsApi.Model;

using MediatR;

using static BuildVersionsApi.Mediators.AddProjectMediator;

public sealed class AddProjectMediator :
    IRequestHandler<AddProjectRequest, MediatorResponse>
{
    public sealed record AddProjectRequest(string ProjectName, int Major, int Minor, int Build, int Revision, string SemanticVersionText) : IRequest<MediatorResponse>
    {
        public static AddProjectRequest Instance(string projectName, int major, int minor, int build, int revision, string semanticVersionText)
        {
            return new(projectName, major, minor, build, revision, semanticVersionText);
        }
    }

    private readonly IPersistanceService service;

    public AddProjectMediator(IPersistanceService service)
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
}

