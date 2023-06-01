namespace BuildVersionsApi.Mediators;

using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Contracts;
using BuildVersionsApi.Data.Services;
using BuildVersionsApi.Mappings;
using BuildVersionsApi.Model;

using MediatR;

public sealed class GetBuildVersionByNameMediator :
    IRequestHandler<GetBuildVersionByNameMediator.GetBuildVersionByNameRequest, MediatorResponse>
{
    public sealed record GetBuildVersionByNameRequest(string ProjectName) : IRequest<MediatorResponse>
    {
        public static GetBuildVersionByNameRequest Instance(string projectName) => new(projectName);
    };

    private readonly IPersistanceService service;

    public GetBuildVersionByNameMediator(IPersistanceService service) => this.service = service;

    public async Task<MediatorResponse> Handle(GetBuildVersionByNameRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.GetByName(request.ProjectName);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }
}
