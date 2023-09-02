namespace TaskManager.API.Model.Domain;

public class Note : BaseEntity
{
    public string Notes { get; set; } = default!;
    public Person PersonTaken { get; set; } = default!;
}
