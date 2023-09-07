using TaskStatus = TaskManager.API.Model.Domain.TaskStatus;

namespace TaskManager.API.Model.Dtos.Task;

public record TaskGet(int Id, string Name, DateTime DeadLine, string TaskStatus, int PersonTakenId);
