using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models;

[Table("comments")]
public class Comment
{
    [Column("id")]
    public int Id { get; set; }

    [Column("task_id")]
    public int TaskId { get; set; }

    [Column("text")]
    public string Text { get; set; } = "";

    [Column("user_login")]
    public string UserLogin { get; set; } = "";

    public User? User { get; set; }
}
