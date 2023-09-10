using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Model.Dtos.Person;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private IPersonService PersonService { get; }

    public PersonController(IPersonService personService)
    {
        PersonService = personService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreatePerson([FromQuery] PersonCreate personCreate)
    {
        var id = await PersonService.CreatePersonAsync(personCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdatePerson([FromQuery] PersonUpdate personUpdate)
    {
        await PersonService.UpdatePersonAsync(personUpdate);
        return Ok();
    }

    [HttpPut]
    [Route("DoneTask")]
    public async Task<IActionResult> DoneTask(int personId, int taskId)
    {
        await PersonService.DoneTaskByIdAsync(personId, taskId);
        return Ok();
    }

    /*[HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeletePerson([FromQuery] PersonDelete personDelete)
    {
        await PersonService.DeletePersonAsync(personDelete);
        return Ok();
    }*/

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetPerson(int id, bool? filterTask)
    {
        var person = await PersonService.GetPersonAsync(id, filterTask);
        return Ok(person);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetPersons([FromQuery] bool? filterTask)
    {
        var persons = await PersonService.GetPersonsAsync(filterTask);
        return Ok(persons);
    }
}
