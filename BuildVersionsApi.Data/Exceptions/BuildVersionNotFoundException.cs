namespace BuildVersionsApi.Data.Exceptions;

using System.Runtime.Serialization;

[Serializable]
internal sealed class BuildVersionNotFoundException : Exception
{
    public BuildVersionNotFoundException()
    {
    }

    public BuildVersionNotFoundException(string? message) : base(message)
    {
    }

    public BuildVersionNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public BuildVersionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}