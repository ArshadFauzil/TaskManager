using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Contracts.Task;
using TaskManager.Services.Tasks;

namespace TaskManager.Controllers;

public class TasksController : ApiController
{
    private readonly ITaskService _taskservice;

    public TasksController(ITaskService _taskservice)
    {
        this._taskservice = _taskservice;
    }

    [HttpPost]
    public IActionResult CreateTask(CreateTaskRequest request)
    {
        ErrorOr<Guid> newTaskCreationResult = _taskservice.createTask(request);

        return newTaskCreationResult.Match(
            newTaskId => getTaskCreatedAtActionResult(newTaskCreationResult.Value),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public IActionResult GetTask(Guid id)
    {
        ErrorOr<TaskResponse> taskRetrievalResult = _taskservice.getTask(id);

        return taskRetrievalResult.Match(
            taskResponse => Ok(taskResponse),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(Guid id, UpdateTaskRequest request)
    {
        ErrorOr<Updated> updateResult = _taskservice.UpdateTask(id, request);

        return updateResult.Match(
            updated => Ok(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(Guid id)
    {
        ErrorOr<Deleted> deleteResult = _taskservice.DeleteTask(id);

        return deleteResult.Match(
            deleted => Ok(),
            errors => Problem(errors)
        );
    }


    //COMMENTS

    [HttpPost]
    [Route("comments")]
    public IActionResult CreateTaskComment(CreateTaskCommentRequest request)
    {
        ErrorOr<Guid> newTaskCommentCreationResult = _taskservice.createTaskComment(request);

        return newTaskCommentCreationResult.Match(
            newTaskCommentId => getTaskCommentCreatedAtActionResult(newTaskCommentId, request.TaskId),
            errors => Problem(errors)
        );
    }

    [HttpGet("{taskId}/comments")]
    public IActionResult GetTaskCommentsByTaskId(Guid taskId)
    {
        ErrorOr<List<TaskCommentResponse>> commentsRetrievalResult = _taskservice.getUserTaskCommentsByTaskId(taskId);

        return commentsRetrievalResult.Match(
            taskCommentsResponse =>
            {
                List<TaskCommentResponse> comments = taskCommentsResponse;
                return comments.Any() ? Ok(comments) : NoContent();
            },
            errors => Problem(errors)
        );
    }

    [HttpPut("comments/{id}")]
    public IActionResult UpdateTaskComment(Guid id, UpdateTaskCommentRequest request)
    {
        ErrorOr<Updated> updateResult = _taskservice.UpdateTaskComment(id, request);

        return updateResult.Match(
            updated => Ok(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("comments/{id}")]
    public IActionResult DeleteTaskComment(Guid id)
    {
        ErrorOr<Deleted> deleteResult = _taskservice.DeleteTaskComment(id);

        return deleteResult.Match(
            deleted => Ok(),
            errors => Problem(errors)
        );
    }


    private CreatedAtActionResult getTaskCreatedAtActionResult(Guid newTaskId)
    {
        return CreatedAtAction(
            actionName: nameof(GetTask),
            routeValues: new { id = newTaskId },
            value: newTaskId);
    }

    private CreatedAtActionResult getTaskCommentCreatedAtActionResult(Guid newCommentId, Guid taskId)
    {
        return CreatedAtAction(
            null,
            null,
            value: newCommentId);
    }


}