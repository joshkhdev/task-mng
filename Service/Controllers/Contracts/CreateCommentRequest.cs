using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.Contracts;

public record CreateCommentRequest
{
    [Required]
    public string Text { get; set; } = "";
}
