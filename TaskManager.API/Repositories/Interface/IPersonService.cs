using TaskManager.API.Model.Dtos.Person;

namespace TaskManager.API.Repositories.Interface;

public interface IPersonService
{
    Task<int> CreatePersonAsync(PersonCreate personCreate);
    Task UpdatePersonAsync(PersonUpdate personUpdate);
    Task<List<PersonDetails>> GetPersonsAsync();
    Task<PersonDetails> GetPersonAsync(int id);
    Task DeletePersonAsync(PersonDelete personDelete);
}
