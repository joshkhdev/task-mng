using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using TaskManager.Models;

namespace TaskManager.Controllers.Contracts;

[DataContract]
public record TaskShortResponse
{
    [DataMember, Required]
    public int Id { get; set; }

    [DataMember, Required]
    public string Name { get; set; } = "";

    [DataMember, Required]
    public TaskEntityStatus Status { get; set; } = TaskEntityStatus.Created;

    [DataMember, Required]
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

[DataContract]
public record TaskResponse : TaskShortResponse
{
    [DataMember, Required]
    public string Description { get; set; } = "";

    [DataMember, Required]
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;

    [DataMember]
    public DateTimeOffset? CompleteDate { get; set; }

    [DataMember]
    public DateTimeOffset? ActualTimeSpent { get; set; }

    [DataMember, Required]
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
