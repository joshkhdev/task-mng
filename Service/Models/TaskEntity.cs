using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models;

[Table("tasks")]
public class TaskEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = "";

    [Column("description")]
    public string Description { get; set; } = "";

    [Column("creation_date")]
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;

    [Column("complete_date")]
    public DateTimeOffset? CompleteDate { get; set; }

    [Column("status")]
    public TaskEntityStatus Status { get; set; } = TaskEntityStatus.Created;

    [Column("planned_completion_date")]
    public DateTimeOffset PlannedCompletionDate { get; set; }

    [Column("actual_time_spent")]
    public DateTimeOffset? ActualTimeSpent { get; set; }

    [Column("user_login")]
    public string UserLogin { get; set; } = "";

    public User? User { get; set; }

    public ICollection<Comment> Comments { get; set; } = [];
}
