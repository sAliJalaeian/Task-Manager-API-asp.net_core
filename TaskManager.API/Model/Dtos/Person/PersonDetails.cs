using TaskManager.API.Model.Dtos.Note;
using TaskManager.API.Model.Dtos.Task;

namespace TaskManager.API.Model.Dtos.Person;

public record PersonDetails(int Id, string Name, List<TaskGetForPerson> Tasks, List<NoteGetForPerson> Notebook);
