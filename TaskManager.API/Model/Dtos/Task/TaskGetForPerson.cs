namespace TaskManager.API.Model.Dtos.Task;

public record TaskGetForPerson(int Id, string Name, DateTime DeadLine, string TaskStatus);
