namespace TaskManager.Models;

public class PasswordHash
{
    public required string Hash { get; set; }

    public required string Salt { get; set; }
}
