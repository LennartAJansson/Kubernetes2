namespace BuildVersionsApi.Mediators;

using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Contracts;
using BuildVersionsApi.Data.Services;
using BuildVersionsApi.Mappings;
using BuildVersionsApi.Model;

using MediatR;

public sealed class UpdateProjectMediator :
    IRequestHandler<UpdateProjectMediator.UpdateProjectRequest, MediatorResponse>
{
    public sealed record UpdateProjectRequest(int Id, string ProjectName, int Major, int Minor, int Build, int Revision, string SemanticVersionText) : IRequest<MediatorResponse>
    {
        public static UpdateProjectRequest Instance(int id, string projectName, int major, int minor, int build, int revision, string semanticVersionText) => new(id, projectName, major, minor, build, revision, semanticVersionText);
    }
    private readonly IPersistanceService service;

    public UpdateProjectMediator(IPersistanceService service) => this.service = service;

    public async Task<MediatorResponse> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.UpdateProject(request.Id, request.ProjectName, request.Major, request.Minor, request.Build, request.Revision, request.SemanticVersionText);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }
}
