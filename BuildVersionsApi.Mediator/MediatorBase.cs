namespace Containers.Common.Mediator;

using BuildVersionsApi.Contract;

public class MediatorBase
{
    protected virtual MediatorResponse CreateResponse(int status, string message, object? data)
    {
        return MediatorResponse.Instance(status, message, data);
    }
}