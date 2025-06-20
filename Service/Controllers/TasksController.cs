using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers.Contracts;
using TaskManager.Services;
using TaskManager.Utils.Extensions;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class TasksController : ControllerBase
{
    private readonly TasksService _tasksService;
    public TasksController(TasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(Name = nameof(GetAllTasks))]
    public async Task<ActionResult<ICollection<TaskShortResponse>>> GetAllTasks(CancellationToken token)
    {
        var tasks = await _tasksService.GetAllTasks(token);

        return Ok(tasks);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}", Name = nameof(GetTaskInfo))]
    public async Task<ActionResult<TaskResponse>> GetTaskInfo(
        [FromRoute, Required] int id,
        CancellationToken token)
    {
        var task = await _tasksService.GetTaskWithComments(id, token);

        return Ok(new TaskResponse(task));
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost(Name = nameof(CreateTask))]
    public async Task<ActionResult<TaskResponse>> CreateTask(
        [FromBody, Required] CreateTaskRequest createRequest,
        CancellationToken token)
    {
        var task = await _tasksService.CreateTask(User.GetUserLogin(), createRequest, token);

        return Ok(new TaskResponse(task));
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpPatch("{id:int}", Name = nameof(UpdateTask))]
    public async Task<ActionResult<TaskResponse>> UpdateTask(
        [FromRoute, Required] int id,
        [FromBody, Required] UpdateTaskRequest updateRequest,
        CancellationToken token)
    {
        await _tasksService.UpdateTask(id, updateRequest, token);

        return NoContent();
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}", Name = nameof(DeleteTask))]
    public async Task<ActionResult<TaskResponse>> DeleteTask(
        [FromRoute, Required] int id,
        CancellationToken token)
    {
        await _tasksService.DeleteTask(id, token);

        return NoContent();
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPost("{taskId:int}/comments", Name = nameof(CreateComment))]
    public async Task<ActionResult<TaskResponse>> CreateComment(
        [FromRoute, Required] int taskId,
        [FromBody, Required] CreateCommentRequest createRequest,
        CancellationToken token)
    {
        await _tasksService.CreateComment(taskId, User.GetUserLogin(), createRequest, token);

        return NoContent();
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [HttpDelete("{taskId:int}/comments/{commentId:int}", Name = nameof(DeleteComment))]
    public async Task<ActionResult<TaskResponse>> DeleteComment(
        [FromRoute, Required] int taskId,
        [FromRoute, Required] int commentId,
        CancellationToken token)
    {
        await _tasksService.DeleteComment(taskId, commentId, token);

        return NoContent();
    }
}
