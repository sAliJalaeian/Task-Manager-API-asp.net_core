using TaskManager.API.Model.Dtos.Person;

namespace TaskManager.API.Model.Dtos.Note;

public record NoteGet(int Id, string Notes, int PersonTakenId);
