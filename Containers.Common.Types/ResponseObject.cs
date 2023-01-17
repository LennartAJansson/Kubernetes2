namespace Containers.Common.Types;

public record ResponseObject(bool Succeeded, string? Message, object? Data)
{
    public static ResponseObject Instance(bool succeeded, string? message, object? data)
    {
        return new(succeeded, message, data);
    }
}
