using System.Runtime.Serialization;

namespace TaskManager.API.Exceptions;

[Serializable]
public class TaskNotFoundException : Exception
{
    public int Id { get; }

    public TaskNotFoundException()
    {
    }

    public TaskNotFoundException(int id)
    {
        Id = id;
    }

    public TaskNotFoundException(string? message) : base(message)
    {
    }

    public TaskNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected TaskNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}