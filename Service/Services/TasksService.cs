using Microsoft.EntityFrameworkCore;
using TaskManager.Controllers.Contracts;
using TaskManager.Models;
using TaskManager.Utils.Exceptions;

namespace TaskManager.Services;

public class TasksService
{
    private readonly TaskManagerDbContext _context;

    public TasksService(TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<TaskShortResponse>> GetAllTasks(CancellationToken token)
    {
        return await _context.Tasks
            .Select(task => new TaskShortResponse(task))
            .ToListAsync(token);
    }

    public async Task<TaskEntity> GetTask(int id, CancellationToken token)
    {
        return await _context.Tasks.FindAsync(id, token)
            ?? throw new RestNotFoundException("Task not found");
    }

    public async Task<TaskEntity> GetTaskWithComments(int id, CancellationToken token)
    {
        return await _context.Tasks
            .Include(task => task.Comments)
            .ThenInclude(comment => comment.User)
            .FirstOrDefaultAsync(task => task.Id == id, token)
            ?? throw new RestNotFoundException("Task not found");
    }

    public async Task<TaskEntity> CreateTask(string userLogin, CreateTaskRequest createRequest, CancellationToken token)
    {
        var task = new TaskEntity
        {
            Name = createRequest.Name,
            Description = createRequest.Description,
            PlannedCompletionDate = createRequest.PlannedCompletionDate,
            CreationDate = DateTimeOffset.Now,
            UserLogin = userLogin,
        };

        task = _context.Tasks.Add(task).Entity;
        await _context.SaveChangesAsync(token);

        return task;
    }

    public async Task UpdateTask(int id, UpdateTaskRequest updateRequest, CancellationToken token)
    {
        var task = await GetTask(id, token);

        if (updateRequest.Status.HasValue && task.Status != updateRequest.Status.Value)
        {
            task.Status = updateRequest.Status.Value;

            if (task.Status == TaskEntityStatus.Completed)
            {
                task.CompleteDate = DateTimeOffset.Now;
            }
            else
            {
                task.CompleteDate = null;
            }
        }

        if (updateRequest.ActualTimeSpent.HasValue)
        {
            task.ActualTimeSpent = updateRequest.ActualTimeSpent.Value;
        }

        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteTask(int id, CancellationToken token)
    {
        var task = await GetTask(id, token);

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(token);
    }

    public async Task CreateComment(
        int taskId,
        string userLogin,
        CreateCommentRequest createRequest,
        CancellationToken token)
    {
        var task = await GetTask(taskId, token);

        var comment = new Comment
        {
            TaskId = task.Id,
            Text = createRequest.Text,
            UserLogin = userLogin,
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteComment(int taskId, int id, CancellationToken token)
    {
        var task = await GetTask(taskId, token);

        var comment = await _context.Comments.FindAsync(id, token)
            ?? throw new RestNotFoundException("Comment not found");

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(token);
    }
}
