namespace BuildVersionsApi.Data.Exceptions;

using System.Runtime.Serialization;

[Serializable]
internal class BuildVersionNotFoundException : Exception
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

    protected BuildVersionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}