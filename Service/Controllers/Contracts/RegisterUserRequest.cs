using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.Contracts;

public record RegisterUserRequest
{
    [Required]
    public required string Login { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string Name { get; set; }
}
