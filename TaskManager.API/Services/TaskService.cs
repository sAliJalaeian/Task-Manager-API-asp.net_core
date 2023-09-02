using AutoMapper;
using FluentValidation;
using TaskManager.API.Exceptions;
using TaskManager.API.Model.Dtos.Task;
using TaskManager.API.Repositories.Interface;
using TaskManager.API.Validation;

namespace TaskManager.API.Services
{
    public class TaskService : ITaskService
    {
        public IMapper Mapper { get; }
        public IGenericRepository<Model.Domain.Task> TaskRepository { get; }
        private TaskCreateValidator TaskCreateValidator { get; }
        public TaskUpdateValidator TaskUpdateValidator { get; }

        public TaskService(IMapper mapper, IGenericRepository<Model.Domain.Task> taskRepository,
            TaskCreateValidator taskCreateValidator, TaskUpdateValidator taskUpdateValidator)
        {
            Mapper = mapper;
            TaskRepository = taskRepository;
            TaskCreateValidator = taskCreateValidator;
            TaskUpdateValidator = taskUpdateValidator;
        }

        public async Task<int> CreateTaskAsync(TaskCreate taskCreate)
        {
            await TaskCreateValidator.ValidateAndThrowAsync(taskCreate);

            var entity = Mapper.Map<Model.Domain.Task>(taskCreate);
            await TaskRepository.InsertAsync(entity);
            await TaskRepository.SaveCangesAsync();
            return entity.Id;
        }

        public async Task DeleteTaskAsync(TaskDelete taskDelete)
        {
            var entity = await TaskRepository.GetByIdAsync(taskDelete.Id);

            if (entity == null)
                throw new TaskNotFoundException(taskDelete.Id);

            TaskRepository.Delete(entity);
            await TaskRepository.SaveCangesAsync();
        }

        public async Task<TaskGet> GetTaskAsync(int id)
        {
            var entity = await TaskRepository.GetByIdAsync(id);

            if (entity == null)
                throw new TaskNotFoundException(id);

            return Mapper.Map<TaskGet>(entity);
        }

        public async Task<List<TaskGet>> GetTasksAsync()
        {
            var entities = await TaskRepository.GetAsync(null, null);
            return Mapper.Map<List<TaskGet>>(entities);
        }

        public async Task UpdateTaskAsync(TaskUpdate taskUpdate)
        {
            await TaskUpdateValidator.ValidateAndThrowAsync(taskUpdate);

            var existingTask = await TaskRepository.GetByIdAsync(taskUpdate.Id);

            if (existingTask == null)
                throw new TaskNotFoundException(taskUpdate.Id);
        
            var entity = Mapper.Map<Model.Domain.Task>(taskUpdate);
            TaskRepository.Update(entity);
            await TaskRepository.SaveCangesAsync();
        }
    }
}
