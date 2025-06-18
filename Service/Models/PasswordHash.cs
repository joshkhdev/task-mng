using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models;

public class PasswordHash
{
    [Column("password_hash")]
    public required string Hash { get; set; }

    [Column("password_salt")]
    public required string Salt { get; set; }
}
