using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.Contracts;

public record LoginUserRequest
{
    [Required]
    public required string Login { get; set; }

    [Required]
    public required string Password { get; set; }
}
