namespace TaskManager.API.Model.Dtos.Task;

public record TaskUpdate(int Id, string Name, string DeadLine, int PersonId);
