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
    public async Task<IActionResult> CreatePerson(PersonCreate personCreate)
    {
        var id = await PersonService.CreatePersonAsync(personCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdatePerson(PersonUpdate personUpdate)
    {
        await PersonService.UpdatePersonAsync(personUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeletePerson(PersonDelete personDelete)
    {
        await PersonService.DeletePersonAsync(personDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetPerson(int id)
    {
        var person = await PersonService.GetPersonAsync(id);
        return Ok(person);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetPersons()
    {
        var persons = await PersonService.GetPersonsAsync();
        return Ok(persons);
    }
}
