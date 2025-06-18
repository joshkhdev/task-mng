using TaskManager.Models;

namespace TaskManager.Controllers.Contracts;

public record UpdateTaskRequest
{
    public TaskEntityStatus? Status { get; set; }

    public DateTimeOffset? ActualTimeSpent { get; set; }
}
