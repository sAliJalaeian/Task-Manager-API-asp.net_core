namespace TaskManager.API.Model.Dtos.Person;

public record PersonFilter(string? Name, string? Task, string? Note, int? Skip, int? Take);