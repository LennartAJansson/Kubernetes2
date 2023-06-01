namespace BuildVersionsApi.Mediators;

using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Contracts;
using BuildVersionsApi.Data.Services;
using BuildVersionsApi.Mappings;
using BuildVersionsApi.Model;

using Containers.Common.Types;

using MediatR;

public sealed class IncreaseMediator :
    IRequestHandler<IncreaseMediator.IncreaseRequest, MediatorResponse>
{
    public sealed record IncreaseRequest(string ProjectName, VersionNumber Number) : IRequest<MediatorResponse>
    {
        public static IncreaseRequest Instance(string projectName, VersionNumber number) => new(projectName, number);
    };

    private readonly IPersistanceService service;

    public IncreaseMediator(IPersistanceService service) => this.service = service;

    public async Task<MediatorResponse> Handle(IncreaseRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.IncreaseVersion(request.ProjectName, request.Number);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }
}
