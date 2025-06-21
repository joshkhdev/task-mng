using System.ComponentModel.DataAnnotations;
using TaskManager.Models;

namespace TaskManager.Controllers.Contracts;

public record CommentResponse
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; } = "";

    [Required]
    public string UserLogin { get; set; } = "";

    [Required]
    public string UserName { get; set; } = "";

    public CommentResponse()
    {
    }

    public CommentResponse(Comment comment)
    {
        Id = comment.Id;
        Text = comment.Text;
        UserLogin = comment.UserLogin;
        UserName = comment.User?.Name ?? "";
    }
}
