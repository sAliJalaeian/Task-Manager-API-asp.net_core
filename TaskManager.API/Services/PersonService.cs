using AutoMapper;
using FluentValidation;
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

        var existingEntity = await PersonRepository.GetByIdAsync(personUpdate.Id, (person) => person.Tasks, (person) => person.Notebook);

        if (existingEntity == null)
            throw new PersonNotFoundException(personUpdate.Id);

        existingEntity.Name = personUpdate.Name;
        PersonRepository.Update(existingEntity);
        await PersonRepository.SaveChangesAsync();
    }
}
