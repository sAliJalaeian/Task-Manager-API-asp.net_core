using AutoMapper;
using FluentValidation;
using TaskManager.API.Exceptions;
using TaskManager.API.Model.Dtos.Task;
using TaskManager.API.Repositories.Interface;
using TaskManager.API.Validation;
using TaskManager.API.Model.Domain;
using TaskManager.API.Model.Dtos.Person;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.API.Services
{
    public class TaskService : ITaskService
    {
        private IMapper Mapper { get; }
        private IGenericRepository<Model.Domain.Task> TaskRepository { get; }
        private IGenericRepository<Person> PersonRepository { get; }
        private TaskCreateValidator TaskCreateValidator { get; }
        private TaskUpdateValidator TaskUpdateValidator { get; }

        public TaskService(IMapper mapper, IGenericRepository<Model.Domain.Task> taskRepository, IGenericRepository<Person> personRepository,
            TaskCreateValidator taskCreateValidator, TaskUpdateValidator taskUpdateValidator)
        {
            Mapper = mapper;
            TaskRepository = taskRepository;
            PersonRepository = personRepository;
            TaskCreateValidator = taskCreateValidator;
            TaskUpdateValidator = taskUpdateValidator;
        }

        public async Task<int> CreateTaskAsync(TaskCreate taskCreate)
        {
            await TaskCreateValidator.ValidateAndThrowAsync(taskCreate);

            var person = await PersonRepository.GetByIdAsync(taskCreate.PersonId);

            if (person == null)
                throw new PersonNotFoundException(taskCreate.PersonId);

            var entity = Mapper.Map<Model.Domain.Task>(taskCreate);
            entity.PersonTaken = person;
            await TaskRepository.InsertAsync(entity);
            await TaskRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteTaskAsync(TaskDelete taskDelete)
        {
            var entity = await TaskRepository.GetByIdAsync(taskDelete.Id);

            if (entity == null)
                throw new TaskNotFoundException(taskDelete.Id);

            TaskRepository.Delete(entity);
            await TaskRepository.SaveChangesAsync();
        }

        public async Task<TaskGet> GetTaskAsync(int id)
        {
            var entity = await TaskRepository.GetByIdAsync(id);

            if (entity == null)
                throw new TaskNotFoundException(id);

            return Mapper.Map<TaskGet>(entity);
        }
        
        public async Task<PersonGet> GetPersonByNoteIdAsync(int id)
        {
            var entity = await TaskRepository.GetByIdAsync(id, (note) => note.PersonTaken);
            if (entity == null)
                throw new TaskNotFoundException(id);
            var person = await PersonRepository.GetByIdAsync(entity.PersonTaken.Id);
            if (person == null)
                throw new PersonNotFoundException(id);
            return Mapper.Map<PersonGet>(person);
        }

        public async Task<List<TaskGet>> GetTasksAsync()
        {
            var entities = await TaskRepository.GetAsync(null, null);
            return Mapper.Map<List<TaskGet>>(entities);
        }

        public async Task UpdateTaskAsync(TaskUpdate taskUpdate)
        {
            await TaskUpdateValidator.ValidateAndThrowAsync(taskUpdate);
            
            var person = await PersonRepository.GetByIdAsync(taskUpdate.PersonId);

            if (person == null)
                throw new PersonNotFoundException(taskUpdate.PersonId);

            var existingTask = await TaskRepository.GetByIdAsync(taskUpdate.Id);

            if (existingTask == null)
                throw new TaskNotFoundException(taskUpdate.Id);
            
            existingTask.PersonTaken = person;
            existingTask.Name = taskUpdate.Name;
            existingTask.DeadLine = taskUpdate.DeadLine;
            TaskRepository.Update(existingTask);
            await TaskRepository.SaveChangesAsync();
        }
    }
}
