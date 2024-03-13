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
            newTaskCommentId => getTaskCommentCreatedAtActionResult(newTaskCommentId),
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


    //FILES

    [HttpPost]
    [Route("files")]
    public async Task<IActionResult> CreateTaskFile([FromForm] CreateTaskFilesRequest request)
    {
        ErrorOr<List<Guid>> newTaskFileCreationResult = await _taskservice.CreateUserTaskFile(request);

        return newTaskFileCreationResult.Match(
            newTaskFileIds => getTaskFileCreatedAtActionResult(newTaskFileIds),
            errors => Problem(errors)
        );
    }

    [HttpGet("{taskId}/files")]
    public async Task<IActionResult> GetTaskFilesByTaskId(Guid taskId)
    {
        ErrorOr<TaskFileResponse> fileRetrievalResult = await _taskservice.getUserTaskFilesByTaskId(taskId);

        return fileRetrievalResult.Match(
            taskFileResponse =>
            {
                return taskFileResponse.files.Count > 0 ? Ok(taskFileResponse) : NoContent();
            },
            errors => Problem(errors)
        );
    }

    [HttpDelete("files/{id}")]
    public IActionResult DeleteTaskFile(Guid id)
    {
        ErrorOr<Deleted> deleteResult = _taskservice.DeleteTaskFile(id);

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

    private CreatedAtActionResult getTaskCommentCreatedAtActionResult(Guid newCommentId)
    {
        return CreatedAtAction(
            null,
            null,
            value: newCommentId);
    }

    private CreatedAtActionResult getTaskFileCreatedAtActionResult(List<Guid> newFileIds)
    {
        return CreatedAtAction(
            null,
            null,
            value: newFileIds);
    }


}