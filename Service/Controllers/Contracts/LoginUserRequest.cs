namespace TaskManager.Controllers.Contracts;

public record LoginUserRequest
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}
