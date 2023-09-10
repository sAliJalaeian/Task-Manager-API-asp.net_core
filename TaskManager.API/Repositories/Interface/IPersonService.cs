using TaskManager.API.Model.Dtos.Person;

namespace TaskManager.API.Repositories.Interface;

public interface IPersonService
{
    Task<int> CreatePersonAsync(PersonCreate personCreate);
    Task UpdatePersonAsync(PersonUpdate personUpdate);
    Task<List<PersonDetails>> GetPersonsAsync(bool? filterTask);
    Task<PersonDetails> GetPersonAsync(int id, bool? filterTask);
    Task DeletePersonAsync(PersonDelete personDelete);
    Task DoneTaskByIdAsync(int personId, int taskId);
}
