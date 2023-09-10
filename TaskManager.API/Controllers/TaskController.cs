using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
    public async Task<IActionResult> CreateTask([FromQuery]TaskCreate taskCreate)
    {
        var id = await TaskService.CreateTaskAsync(taskCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateTask([FromQuery]TaskUpdate taskUpdate)
    {
        await TaskService.UpdateTaskAsync(taskUpdate);
        return Ok();
    }

    [HttpPut]
    [Route("GiveTaskToPerson")]
    public async Task<IActionResult> GiveTaskToPerson(int taskId, int personId)
    {
        await TaskService.GiveTaskToPersonAsync(taskId, personId);
        return Ok();
    }

    /*[HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteTask([FromQuery] TaskDelete taskDelete)
    {
        await TaskService.DeleteTaskAsync(taskDelete);
        return Ok();
    }*/

    [HttpPut]
    [Route("Expire/Deadline")]
    public async Task<IActionResult> ExpireByDeadline()
    {
        PersianCalendar pc = new PersianCalendar();
        var currentDate = DateTime.Now;
        await TaskService.ExpireTaskByDeadline(new DateTime(pc.GetYear(currentDate), pc.GetMonth(currentDate), pc.GetDayOfMonth(currentDate)));
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
