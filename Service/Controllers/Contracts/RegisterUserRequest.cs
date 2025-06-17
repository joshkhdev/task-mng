namespace TaskManager.Controllers.Contracts;

public record RegisterUserRequest
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public required string Name { get; set; }
}
