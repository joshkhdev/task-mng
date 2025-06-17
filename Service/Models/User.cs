namespace TaskManager.Models;

public class User
{
    public required string Login { get; set; }

    public required PasswordHash Password { get; set; }

    public required string Name { get; set; }
}
