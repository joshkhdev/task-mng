using TaskManager.Models;

namespace TaskManager.Controllers.Contracts;

public record TaskShortResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public TaskEntityStatus Status { get; set; } = TaskEntityStatus.Created;

    public DateTimeOffset PlannedCompletionDate { get; set; }

    public TaskShortResponse()
    {
    }

    public TaskShortResponse(TaskEntity task)
    {
        Id = task.Id;
        Name = task.Name;
        Status = task.Status;
        PlannedCompletionDate = task.PlannedCompletionDate;
    }
}

public record TaskResponse : TaskShortResponse
{

    public string Description { get; set; } = "";

    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset? CompleteDate { get; set; }

    public DateTimeOffset? ActualTimeSpent { get; set; }

    public IEnumerable<CommentResponse> Comments { get; set; } = [];

    public TaskResponse()
    {
    }

    public TaskResponse(TaskEntity task)
    {
        Id = task.Id;
        Name = task.Name;
        Description = task.Description;
        CreationDate = task.CreationDate;
        CompleteDate = task.CompleteDate;
        Status = task.Status;
        PlannedCompletionDate = task.PlannedCompletionDate;
        ActualTimeSpent = task.ActualTimeSpent;
        Comments = task.Comments.Select(c => new CommentResponse(c));
    }
}
