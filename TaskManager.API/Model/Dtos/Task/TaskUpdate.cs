namespace TaskManager.API.Model.Dtos.Task;

public record TaskUpdate(int Id, string Name, DateTime DeadLine, int PersonId);
