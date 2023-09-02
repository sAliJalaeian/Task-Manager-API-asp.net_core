using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Model.Dtos.Note;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    private INoteService NoteService { get; }

    public NoteController(INoteService noteService)
    {
        NoteService = noteService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateNote(NoteCreate noteCreate)
    {
        var id = await NoteService.CreateNoteAsync(noteCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateNote(NoteUpdate noteUpdate)
    {
        await NoteService.UpdateNoteAsync(noteUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteNote(NoteDelete noteDelete)
    {
        await NoteService.DeleteNoteAsync(noteDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetNote(int id)
    {
        var note = await NoteService.GetNoteAsync(id);
        return Ok(note);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetNotes()
    {
        var notes = await NoteService.GetNotesAsync();
        return Ok(notes);
    }
}
