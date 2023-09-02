namespace TaskManager.API.Model.Domain;

public class Task : BaseEntity
{
    public string Name { get; set; } = default!;
    public string DeadLine { get; set; } = default!;
    public Person PersonTaken { get; set; } = default!;
}
