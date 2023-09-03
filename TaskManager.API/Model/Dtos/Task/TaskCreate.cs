namespace TaskManager.API.Model.Dtos.Task;

public record TaskCreate(string Name, string DeadLine, int PersonId);
