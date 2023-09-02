using AutoMapper;
using FluentValidation;
using System.Linq.Expressions;
using TaskManager.API.Exceptions;
using TaskManager.API.Model.Domain;
using TaskManager.API.Model.Dtos.Person;
using TaskManager.API.Repositories.Interface;
using TaskManager.API.Validation;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.API.Services;

public class PersonService : IPersonService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Person> PersonRepository { get; }
    private IGenericRepository<Model.Domain.Task> TaskRepository { get; }
    private IGenericRepository<Note> NoteRepository { get; }
    private PersonCreateValidator PersonCreateValidator { get; }
    private PersonUpdateValidator PersonUpdateValidator { get; }

    public PersonService(IMapper mapper, IGenericRepository<Person> personRepository, IGenericRepository<Model.Domain.Task> taskRepository,
        IGenericRepository<Note> noteRepository, PersonCreateValidator personCreateValidator, PersonUpdateValidator personUpdateValidator)
    {
        Mapper = mapper;
        PersonRepository = personRepository;
        TaskRepository = taskRepository;
        NoteRepository = noteRepository;
        PersonCreateValidator = personCreateValidator;
        PersonUpdateValidator = personUpdateValidator;
    }

    public async Task<int> CreatePersonAsync(PersonCreate personCreate)
    {
        await PersonCreateValidator.ValidateAndThrowAsync(personCreate);

        Expression<Func<Model.Domain.Task, bool>> taskFilter = task => personCreate.Tasks.Contains(task.Id);
        var tasks = await TaskRepository.GetFilteredAsync(new[] { taskFilter }, null, null);
        var missingTasks = personCreate.Tasks.Where((id) => !tasks.Any(existing => existing.Id == id));

        if (missingTasks.Any())
            throw new TasksNotFoundException(missingTasks.ToArray());

        Expression<Func<Note, bool>> noteFilter = note => personCreate.Notebook.Contains(note.Id);
        var notes = await NoteRepository.GetFilteredAsync(new[] { noteFilter }, null, null);
        var missingNotes = personCreate.Notebook.Where((id) => !notes.Any(existing => existing.Id == id));

        if (missingNotes.Any())
            throw new NotesNotFoundException(missingNotes.ToArray());

        var person = Mapper.Map<Person>(personCreate);
        person.Tasks = tasks;
        person.Notebook = notes; 
        await PersonRepository.InsertAsync(person);
        await PersonRepository.SaveCangesAsync();
        return person.Id;
    }

    public async Task DeletePersonAsync(PersonDelete personDelete)
    {
        var entity = await PersonRepository.GetByIdAsync(personDelete.Id);

        if (entity == null)
            throw new PersonNotFoundException(personDelete.Id);

        PersonRepository.Delete(entity);
        await PersonRepository.SaveCangesAsync();
    }

    public async Task<PersonDetails> GetPersonAsync(int id)
    {
        var entity = await PersonRepository.GetByIdAsync(id, (person) => person.Tasks, (person) => person.Notebook);

        if (entity == null)
            throw new PersonNotFoundException(id);

        return Mapper.Map<PersonDetails>(entity);
    }

    public async Task<List<PersonDetails>> GetPersonsAsync()
    {
        var entities = await PersonRepository.GetAsync(null, null, (person) => person.Tasks, (person) => person.Notebook);

        return Mapper.Map<List<PersonDetails>>(entities);
    }

    public async Task UpdatePersonAsync(PersonUpdate personUpdate)
    {
        await PersonUpdateValidator.ValidateAndThrowAsync(personUpdate);

        Expression<Func<Model.Domain.Task, bool>> taskFilter = task => personUpdate.Tasks.Contains(task.Id);
        var tasks = await TaskRepository.GetFilteredAsync(new[] { taskFilter }, null, null);
        var missingTasks = personUpdate.Tasks.Where((id) => !tasks.Any(existing => existing.Id == id));

        if (missingTasks.Any())
            throw new TasksNotFoundException(missingTasks.ToArray());

        Expression<Func<Note, bool>> noteFilter = note => personUpdate.Notebook.Contains(note.Id);
        var notes = await NoteRepository.GetFilteredAsync(new[] { noteFilter }, null, null);
        var missingNotes = personUpdate.Notebook.Where((id) => !notes.Any(existing => existing.Id == id));

        if (missingNotes.Any())
            throw new NotesNotFoundException(missingNotes.ToArray());

        var existingEtity = await PersonRepository.GetByIdAsync(personUpdate.Id, (person) => person.Tasks, (person) => person.Notebook);

        if (existingEtity == null)
            throw new PersonNotFoundException(personUpdate.Id);

        Mapper.Map(personUpdate, existingEtity);
        existingEtity.Tasks = tasks;
        existingEtity.Notebook = notes;
        PersonRepository.Update(existingEtity);
        await PersonRepository.SaveCangesAsync();
    }
}
