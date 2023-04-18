namespace BuildVersionsApi.Endpoints;

using BuildVersionsApi.Contracts;

using Containers.Common.Types;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

public static class BuildVersionEndpoints
{
    public static IEndpointRouteBuilder MapBuildVersionEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/buildversions")
            .WithTags(nameof(BuildVersionResponse));

        _ = group.MapPost("/CreateProject",
            async Task<Results<Ok<BuildVersionResponse>, NotFound>> (AddProjectRequest request, IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(request);
                return response.Data is not null ?
                TypedResults.Ok((BuildVersionResponse)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("CreateProject")
            .WithOpenApi();

        _ = group.MapPut("/UpdateProject",
            async Task<Results<Ok<BuildVersionResponse>, NotFound>> (UpdateProjectRequest request, IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(request);
                return response.Data is not null ?
                TypedResults.Ok((BuildVersionResponse)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("UpdateProject")
            .WithOpenApi();

        _ = group.MapGet("/NewMajorVersion/{name}",
            async Task<Results<Ok<BuildVersionResponse>, NotFound>> (string name, IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(IncreaseRequest.Instance(name, VersionNumber.Major));
                return response.Data is not null ?
                TypedResults.Ok((BuildVersionResponse)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("NewMajorVersion")
            .WithOpenApi();

        _ = group.MapGet("/NewMinorVersion/{name}",
            async Task<Results<Ok<BuildVersionResponse>, NotFound>> (string name, IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(IncreaseRequest.Instance(name, VersionNumber.Minor));
                return response.Data is not null ?
                TypedResults.Ok((BuildVersionResponse)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("NewMinorVersion")
            .WithOpenApi();

        _ = group.MapGet("/NewBuildVersion/{name}",
            async Task<Results<Ok<BuildVersionResponse>, NotFound>> (string name, IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(IncreaseRequest.Instance(name, VersionNumber.Build));
                return response.Data is not null ?
                TypedResults.Ok((BuildVersionResponse)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("NewBuildVersion")
            .WithOpenApi();

        _ = group.MapGet("/NewRevisionVersion/{name}",
            async Task<Results<Ok<BuildVersionResponse>, NotFound>> (string name, IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(IncreaseRequest.Instance(name, VersionNumber.Revision));
                return response.Data is not null ?
                TypedResults.Ok((BuildVersionResponse)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("NewRevisionVersion")
            .WithOpenApi();

        _ = group.MapGet("/GetVersionById/{id}",
            async Task<Results<Ok<BuildVersionResponse>, NotFound>> (int id, IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(GetBuildVersionByIdRequest.Instance(id));
                return response.Data is not null ?
                TypedResults.Ok((BuildVersionResponse)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("GetVersionById")
            .WithOpenApi();

        _ = group.MapGet("/GetVersionByName/{name}",
            async Task<Results<Ok<BuildVersionResponse>, NotFound>> (string name, IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(GetBuildVersionByNameRequest.Instance(name));
                return response.Data is not null ?
                TypedResults.Ok((BuildVersionResponse)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("GetVersionByName")
            .WithOpenApi();

        _ = group.MapGet("/GetAll",
            async Task<Results<Ok<IEnumerable<BuildVersionResponse>>, NotFound>> (IMediator mediator) =>
            {
                MediatorResponse response = await mediator.Send(GetAllBuildVersions.Instance());
                return response.Data is not null ?
                TypedResults.Ok((IEnumerable<BuildVersionResponse>)response.Data) :
                TypedResults.NotFound();
            })
            .WithName("GetAll")
            .WithOpenApi();

        return routes;
    }
}