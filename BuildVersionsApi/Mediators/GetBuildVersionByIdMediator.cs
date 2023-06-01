namespace BuildVersionsApi.Mediators;

using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Contracts;
using BuildVersionsApi.Data.Services;
using BuildVersionsApi.Mappings;
using BuildVersionsApi.Model;

using MediatR;

public sealed class GetBuildVersionByIdMediator :
    IRequestHandler<GetBuildVersionByIdMediator.GetBuildVersionByIdRequest, MediatorResponse>
{
    public sealed record GetBuildVersionByIdRequest(int Id) : IRequest<MediatorResponse>
    {
        public static GetBuildVersionByIdRequest Instance(int id) => new(id);
    };

    private readonly IPersistanceService service;

    public GetBuildVersionByIdMediator(IPersistanceService service) => this.service = service;

    public async Task<MediatorResponse> Handle(GetBuildVersionByIdRequest request, CancellationToken cancellationToken)
    {
        BuildVersion? model = await service.GetById(request.Id);

        return model is null
            ? MediatorResponse.Instance(404, "Not found", null)
            : MediatorResponse.Instance(200, "Ok", model.ToBuildVersionResponse());
    }
}
