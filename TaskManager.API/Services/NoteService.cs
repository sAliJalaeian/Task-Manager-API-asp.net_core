using AutoMapper;
using FluentValidation;
using TaskManager.API.Exceptions;
using TaskManager.API.Model.Domain;
using TaskManager.API.Model.Dtos.Note;
using TaskManager.API.Repositories.Interface;
using TaskManager.API.Validation;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.API.Services;

public class NoteService : INoteService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Note> NoteRepository { get; }
    private NoteCreateValidator NoteCreateValidator { get; }
    private NoteUpdateValidator NoteUpdateValidator { get; }

    public NoteService(IMapper mapper, IGenericRepository<Note> noteRepository,
        NoteCreateValidator noteCreateValidator, NoteUpdateValidator noteUpdateValidator)
    {
        Mapper = mapper;
        NoteRepository = noteRepository;
        NoteCreateValidator = noteCreateValidator;
        NoteUpdateValidator = noteUpdateValidator;
    }

    public async Task<int> CreateNoteAsync(NoteCreate noteCreate)
    {
        await NoteCreateValidator.ValidateAndThrowAsync(noteCreate);

        var entity = Mapper.Map<Note>(noteCreate);
        await NoteRepository.InsertAsync(entity);
        await NoteRepository.SaveCangesAsync();
        return entity.Id;
    }

    public async Task DeleteNoteAsync(NoteDelete noteDelete)
    {
        var entity = await NoteRepository.GetByIdAsync(noteDelete.Id);

        if (entity == null)
            throw new NoteNotFoundException(noteDelete.Id);
        NoteRepository.Delete(entity);
        await NoteRepository.SaveCangesAsync();
    }

    public async Task<NoteGet> GetNoteAsync(int id)
    {
        var entity = await NoteRepository.GetByIdAsync(id);

        if (entity == null)
            throw new NoteNotFoundException(id);

        return Mapper.Map<NoteGet>(entity);
    }

    public async Task<List<NoteGet>> GetNotesAsync()
    {
        var entities = await NoteRepository.GetAsync(null, null);
        return Mapper.Map<List<NoteGet>>(entities);
    }

    public async Task UpdateNoteAsync(NoteUpdate noteUpdate)
    {
        await NoteUpdateValidator.ValidateAndThrowAsync(noteUpdate);

        var existingNote = await NoteRepository.GetByIdAsync(noteUpdate.Id);

        if (existingNote == null)
            throw new NoteNotFoundException(noteUpdate.Id);

        var entity = Mapper.Map<Note>(noteUpdate);
        NoteRepository.Update(entity);
        await NoteRepository.SaveCangesAsync();
    }
}
