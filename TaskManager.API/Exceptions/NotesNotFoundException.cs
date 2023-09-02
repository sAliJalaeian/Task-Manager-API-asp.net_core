using System.Runtime.Serialization;

namespace TaskManager.API.Exceptions
{
    [Serializable]
    public class NotesNotFoundException : Exception
    {
        public int[] NoteIds { get; }

        public NotesNotFoundException()
        {
        }

        public NotesNotFoundException(int[] noteIds)
        {
            NoteIds = noteIds;
        }

        public NotesNotFoundException(string? message) : base(message)
        {
        }

        public NotesNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}