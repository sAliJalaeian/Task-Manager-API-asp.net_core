namespace TaskManager.API.Model.Domain;

public class Task : BaseEntity
{
    public string Name { get; set; } = default!;
    public DateTime DeadLine { get; set; } = default!;
    public TaskStatus TaskStatus { get; set; } = default!;
    public Person? PersonTaken { get; set; }
    public int? PersonTakenId { get; set; }
}

public enum TaskStatus
{
    Created,
    InProgress,
    Expired
}
