using System.Runtime.Serialization;

namespace TaskManager.API.Exceptions
{
    [Serializable]
    public class PersonNotFoundException : Exception
    {
        public int Id { get; }

        public PersonNotFoundException()
        {
        }

        public PersonNotFoundException(int id)
        {
            Id = id;
        }

        public PersonNotFoundException(string? message) : base(message)
        {
        }

        public PersonNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PersonNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}