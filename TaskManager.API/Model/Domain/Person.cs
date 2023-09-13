
using Microsoft.AspNetCore.Identity;

namespace TaskManager.API.Model.Domain;

public class Person : BaseEntity
{
    public string Name { get; set; } = default!;
    public List<Task> Tasks { get; set; } = default!;
    public List<Note> Notebook { get; set; } = default!;
}
