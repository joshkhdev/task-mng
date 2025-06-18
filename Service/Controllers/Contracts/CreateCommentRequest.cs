namespace TaskManager.Controllers.Contracts;

public record CreateCommentRequest
{
    public string Text { get; set; } = "";
}
