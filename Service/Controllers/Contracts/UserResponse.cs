namespace TaskManager.Controllers.Contracts;

public record UserResponse
{
    public required string Login { get; set; }

    public required string Name { get; set; }
}
