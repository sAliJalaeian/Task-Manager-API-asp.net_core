using System.Runtime.Serialization;

namespace TaskManager.API.Exceptions
{
    [Serializable]
    public class TasksNotFoundException : Exception
    {
        public int[] TaskIds;

        public TasksNotFoundException()
        {
        }

        public TasksNotFoundException(int[] taskIds)
        {
            TaskIds = taskIds;
        }

        public TasksNotFoundException(string? message) : base(message)
        {
        }

        public TasksNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TasksNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}