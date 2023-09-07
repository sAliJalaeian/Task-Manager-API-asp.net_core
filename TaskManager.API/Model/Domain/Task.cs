namespace TaskManager.API.Model.Domain;

public class Task : BaseEntity
{
    public string Name { get; set; } = default!;
    public DateTime DeadLine { get; set; } = default!;
    public Person? PersonTaken { get; set; }
}
