using TaskManager.API.Model.Dtos.Note;
using TaskManager.API.Model.Dtos.Person;

namespace TaskManager.API.Repositories.Interface;

public interface INoteService
{
    Task<int> CreateNoteAsync(NoteCreate noteCreate);
    Task UpdateNoteAsync(NoteUpdate noteUpdate);
    Task DeleteNoteAsync(NoteDelete noteDelete);
    Task<NoteGet> GetNoteAsync(int id);
    Task<PersonGet> GetPersonByNoteIdAsync(int id);
    Task<List<NoteGet>> GetNotesAsync();
}
