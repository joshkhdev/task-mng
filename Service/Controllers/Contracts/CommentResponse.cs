using TaskManager.Models;

namespace TaskManager.Controllers.Contracts;

public record CommentResponse
{
    public int Id { get; set; }

    public string Text { get; set; } = "";

    public string UserLogin { get; set; } = "";

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
