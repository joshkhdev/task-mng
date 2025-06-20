using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.Contracts;

public record CreateTaskRequest
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public DateTimeOffset PlannedCompletionDate { get; set; }
}
