using Microsoft.AspNetCore.Mvc;
using TaskManager.Enumerations;
using TaskManager.Models;
using TaskManager.Services.Tasks;
using TaskManger.Contracts.Task;

namespace TaskManager.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskservice;

    public TasksController(ITaskService _taskservice)
    {
        this._taskservice = _taskservice;
    }

    [HttpPost]
    public IActionResult CreateTask(CreateTaskRequest request)
    {

        Guid id = Guid.NewGuid();

        UserTask task = new UserTask(
            id,
            request.Title,
            request.Description,
            request.DueDate,
            StatusEnum.COMPLETE,
            DateTime.UtcNow
        );

        _taskservice.createTask(task);

        var response = new TaskResponse(
            task.Id,
            task.Title,
            task.Description,
            task.DueDate,
            nameof(task.Status),
            task.LastUpdatedDate
        );


        return CreatedAtAction(
            actionName: nameof(GetTask),
            routeValues: new { id = task.Id },
            value: response);
    }

    [HttpGet("{id}")]
    public IActionResult GetTask(Guid id)
    {
        UserTask task = _taskservice.getTask(id);

        TaskResponse response = new TaskResponse(
            task.Id,
            task.Title,
            task.Description,
            task.DueDate,
            nameof(task.Status),
            task.LastUpdatedDate
        );

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(Guid id, UpdateTaskRequest updateTaskRequest)
    {
        return Ok(updateTaskRequest);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(Guid id)
    {
        return Ok(id);
    }

}