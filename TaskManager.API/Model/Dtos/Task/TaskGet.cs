using TaskManager.API.Model.Dtos.Person;

namespace TaskManager.API.Model.Dtos.Task;

public record TaskGet(int Id, string Name, string DeadLine);
