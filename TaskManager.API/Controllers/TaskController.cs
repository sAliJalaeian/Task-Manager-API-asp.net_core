using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Model.Dtos.Task;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private ITaskService TaskService { get; }

    public TaskController(ITaskService taskService)
    {
        TaskService = taskService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTask(TaskCreate taskCreate)
    {
        var id = await TaskService.CreateTaskAsync(taskCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateTask(TaskUpdate taskUpdate)
    {
        await TaskService.UpdateTaskAsync(taskUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteTask(TaskDelete taskDelete)
    {
        await TaskService.DeleteTaskAsync(taskDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await TaskService.GetTaskAsync(id);
        return Ok(task);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await TaskService.GetTasksAsync();
        return Ok(tasks);
    }
}
