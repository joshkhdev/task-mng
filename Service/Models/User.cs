using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models;

[Table("users")]
public class User
{
    [Column("login")]
    public required string Login { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    public required PasswordHash Password { get; set; }
}
