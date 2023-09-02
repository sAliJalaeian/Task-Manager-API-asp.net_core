namespace TaskManager.API.Model.Dtos.Person;

public record PersonCreate(string Name, List<int> Tasks, List<int> Notebook);