namespace TaskManager.Controllers.Contracts;

public record CreateTaskRequest
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public DateTimeOffset PlannedCompletionDate { get; set; }
}
