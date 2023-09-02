using TaskManager.API.Model.Dtos.Task;

namespace TaskManager.API.Repositories.Interface;

public interface ITaskService
{
    Task<int> CreateTaskAsync(TaskCreate taskCreate);
    Task UpdateTaskAsync(TaskUpdate taskUpdate);
    Task DeleteTaskAsync(TaskDelete taskDelete);
    Task<TaskGet> GetTaskAsync(int id);
    Task<List<TaskGet>> GetTasksAsync();
}
