namespace TaskManager.API.Model.Dtos.Task;

public record TaskCreate(string Name, DateTime DeadLine, int? PersonId);
