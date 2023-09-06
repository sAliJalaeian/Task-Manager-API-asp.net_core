﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteTask([FromQuery] TaskDelete taskDelete)
    {
        await TaskService.DeleteTaskAsync(taskDelete);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteByDeadline")]
    public async Task<IActionResult> DeleteByDeadline([FromQuery] DateTime today)
    {
        await TaskService.DeleteTaskByDeadline(today);
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
    [Route("GetPersonByNoteId/{id}")]
    public async Task<IActionResult> GetPersonByNoteId(int id)
    {
        var person = await TaskService.GetPersonByNoteIdAsync(id);
        return Ok(person);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await TaskService.GetTasksAsync();
        return Ok(tasks);
    }
}
