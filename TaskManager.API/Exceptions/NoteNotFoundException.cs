using System.Runtime.Serialization;

namespace TaskManager.API.Exceptions
{
    [Serializable]
    public class NoteNotFoundException : Exception
    {
        public int Id { get; }

        public NoteNotFoundException()
        {
        }

        public NoteNotFoundException(int id)
        {
            Id = id;
        }

        public NoteNotFoundException(string? message) : base(message)
        {
        }

        public NoteNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}