using AutoMapper;
using FluentValidation;
using TaskManager.API.Exceptions;
using TaskManager.API.Model.Dtos.Task;
using TaskManager.API.Repositories.Interface;
using TaskManager.API.Validation;
using TaskManager.API.Model.Domain;
using Task = System.Threading.Tasks.Task;
using TaskStatus = TaskManager.API.Model.Domain.TaskStatus;

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

            if (person == null && taskCreate.PersonId != null)
                throw new PersonNotFoundException(taskCreate.PersonId);
            

            var entity = Mapper.Map<Model.Domain.Task>(taskCreate);
            entity.PersonTaken = person;
            if (taskCreate.PersonId == null)
                entity.TaskStatus = TaskStatus.Created;
            else
                entity.TaskStatus = TaskStatus.InProgress;
            await TaskRepository.InsertAsync(entity);
            await TaskRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task GiveTaskToPersonAsync(int taskId, int personId)
        {
            var entity = await TaskRepository.GetByIdAsync(taskId);

            if (entity == null)
                throw new TaskNotFoundException(taskId);

            var person = await PersonRepository.GetByIdAsync(personId, task => task.Tasks);

            if (person == null)
                throw new PersonNotFoundException(personId);
           
            person.Tasks.Add(entity);
            entity.TaskStatus = TaskStatus.InProgress;
            await TaskRepository.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskDelete taskDelete)
        {
            var entity = await TaskRepository.GetByIdAsync(taskDelete.Id);

            if (entity == null)
                throw new TaskNotFoundException(taskDelete.Id);

            TaskRepository.Delete(entity);
            await TaskRepository.SaveChangesAsync();
        }

        public async Task ExpireTaskByDeadline(DateTime deadline)
        {
            var tasks = await TaskRepository.GetAsync(null, null);
            
            if (tasks.Count == 0)
                throw new TasksNotFoundException();
            
            var tasksToDelete = tasks.Where(t => t.DeadLine <= deadline).ToList();
            foreach (var task in tasksToDelete)
                task.TaskStatus = TaskStatus.Expired;
            
            await TaskRepository.SaveChangesAsync();
        }

        public async Task<TaskGet> GetTaskAsync(int id)
        {
            var entity = await TaskRepository.GetByIdAsync(id, task => task.PersonTaken);
            
            if (entity == null)
                throw new TaskNotFoundException(id);

            return Mapper.Map<TaskGet>(entity);
        }

        public async Task<List<TaskGet>> GetTasksAsync()
        {
            var entities = await TaskRepository.GetAsync(null, null, task => task.PersonTaken);
            
            return Mapper.Map<List<TaskGet>>(entities);
        }

        public async Task UpdateTaskAsync(TaskUpdate taskUpdate)
        {
            await TaskUpdateValidator.ValidateAndThrowAsync(taskUpdate);
            
            var person = await PersonRepository.GetByIdAsync(taskUpdate.PersonId);

            if (person == null && taskUpdate.PersonId != null)
                throw new PersonNotFoundException(taskUpdate.PersonId);

            var existingTask = await TaskRepository.GetByIdAsync(taskUpdate.Id);

            if (existingTask == null)
                throw new TaskNotFoundException(taskUpdate.Id);
            
            existingTask.PersonTaken = person;
            existingTask.Name = taskUpdate.Name;
            existingTask.DeadLine = taskUpdate.DeadLine;
            if (taskUpdate.PersonId == null)
                existingTask.TaskStatus = TaskStatus.Created;
            else
                existingTask.TaskStatus = TaskStatus.InProgress;
            TaskRepository.Update(existingTask);
            await TaskRepository.SaveChangesAsync();
        }
    }
}
