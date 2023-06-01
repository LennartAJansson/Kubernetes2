namespace BuildVersionsApi.Mediators;

using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Contracts;
using BuildVersionsApi.Data.Services;
using BuildVersionsApi.Mappings;
using BuildVersionsApi.Model;

using MediatR;

public sealed class GetAllBuildVersionMediator :
    IRequestHandler<GetAllBuildVersionMediator.GetAllBuildVersions, MediatorResponse>
{
    public sealed record GetAllBuildVersions() : IRequest<MediatorResponse>
    {
        public static GetAllBuildVersions Instance() => new();
    };

    private readonly IPersistanceService service;

    public GetAllBuildVersionMediator(IPersistanceService service) => this.service = service;

    public async Task<MediatorResponse> Handle(GetAllBuildVersions request, CancellationToken cancellationToken)
    {
        IEnumerable<BuildVersion> models = await service.GetAll();

        return !models.Any()
            ? MediatorResponse.Instance(404, "Not found", Enumerable.Empty<BuildVersionResponse>())
            : MediatorResponse.Instance(200, "Ok", models.Select(m => m.ToBuildVersionResponse()));
    }
}
