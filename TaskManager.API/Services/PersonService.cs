using AutoMapper;
using FluentValidation;
using TaskManager.API.Exceptions;
using TaskManager.API.Model.Domain;
using TaskManager.API.Model.Dtos.Person;
using TaskManager.API.Repositories.Interface;
using TaskManager.API.Validation;
using Task = System.Threading.Tasks.Task;
using TaskStatus = TaskManager.API.Model.Domain.TaskStatus;

namespace TaskManager.API.Services;

public class PersonService : IPersonService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Person> PersonRepository { get; }
    private PersonCreateValidator PersonCreateValidator { get; }
    private PersonUpdateValidator PersonUpdateValidator { get; }

    public PersonService(IMapper mapper, IGenericRepository<Person> personRepository,
        PersonCreateValidator personCreateValidator, PersonUpdateValidator personUpdateValidator)
    {
        Mapper = mapper;
        PersonRepository = personRepository;
        PersonCreateValidator = personCreateValidator;
        PersonUpdateValidator = personUpdateValidator;
    }

    public async Task<int> CreatePersonAsync(PersonCreate personCreate)
    {
        await PersonCreateValidator.ValidateAndThrowAsync(personCreate);

        var person = Mapper.Map<Person>(personCreate);
        await PersonRepository.InsertAsync(person);
        await PersonRepository.SaveChangesAsync();
        return person.Id;
    }

    public async Task DeletePersonAsync(PersonDelete personDelete)
    {
        var entity = await PersonRepository.GetByIdAsync(personDelete.Id, (person) => person.Tasks, (person) => person.Notebook);

        if (entity == null)
            throw new PersonNotFoundException(personDelete.Id);

        PersonRepository.Delete(entity);
        await PersonRepository.SaveChangesAsync();
    }

    public async Task<PersonDetails> GetPersonAsync(int id, bool? filterTask)
    {
        var entity = new Person();
                
        if (filterTask == null)
            entity = await PersonRepository.GetByIdAsync(id, (person) => person.Tasks.Where(task => task.TaskStatus.Equals(TaskStatus.InProgress) || task.TaskStatus.Equals(TaskStatus.Done)), (person) => person.Notebook);
        else if (filterTask == true)
            entity = await PersonRepository.GetByIdAsync(id, (person) => person.Tasks.Where(task => task.TaskStatus.Equals(TaskStatus.Done)), (person) => person.Notebook);
        else
            entity = await PersonRepository.GetByIdAsync(id, (person) => person.Tasks.Where(task => task.TaskStatus.Equals(TaskStatus.InProgress)), (person) => person.Notebook);
        if (entity == null)
            throw new PersonNotFoundException(id);

        return Mapper.Map<PersonDetails>(entity);
    }

    public async Task<List<PersonDetails>> GetPersonsAsync(bool? filterTask)
    {
        var entities = new List<Person>();

        if (filterTask == null)
            entities = await PersonRepository.GetAsync(null, null, (person) => person.Tasks.Where(task => task.TaskStatus.Equals(TaskStatus.InProgress) || task.TaskStatus.Equals(TaskStatus.Done)), (person) => person.Notebook);
        else if (filterTask == true)
            entities = await PersonRepository.GetAsync(null, null, (person) => person.Tasks.Where(task => task.TaskStatus.Equals(TaskStatus.Done)), (person) => person.Notebook);
        else
            entities = await PersonRepository.GetAsync(null, null, (person) => person.Tasks.Where(task => task.TaskStatus.Equals(TaskStatus.InProgress)), (person) => person.Notebook);

        return Mapper.Map<List<PersonDetails>>(entities);
    }

    public async Task DoneTaskByIdAsync(int personId, int taskId)
    {
        var entity = await PersonRepository.GetByIdAsync(personId, person => person.Tasks.Where(task => task.TaskStatus.Equals(TaskStatus.InProgress)));

        if (entity == null)
            throw new PersonNotFoundException(personId);

        var task = entity.Tasks.Where(task => task.Id == taskId).FirstOrDefault();

        if (task == null)
            throw new TaskNotFoundException(taskId);

        task.TaskStatus = TaskStatus.Done;
        await PersonRepository.SaveChangesAsync();
    }

    public async Task UpdatePersonAsync(PersonUpdate personUpdate)
    {
        await PersonUpdateValidator.ValidateAndThrowAsync(personUpdate);

        var existingEntity = await PersonRepository.GetByIdAsync(personUpdate.Id, (person) => person.Tasks, (person) => person.Notebook);

        if (existingEntity == null)
            throw new PersonNotFoundException(personUpdate.Id);

        existingEntity.Name = personUpdate.Name;
        PersonRepository.Update(existingEntity);
        await PersonRepository.SaveChangesAsync();
    }
}
