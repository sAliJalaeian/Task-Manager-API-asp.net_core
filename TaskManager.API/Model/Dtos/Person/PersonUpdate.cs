namespace TaskManager.API.Model.Dtos.Person;

public record PersonUpdate(int Id, string Name, List<int> Tasks, List<int> Notebook);