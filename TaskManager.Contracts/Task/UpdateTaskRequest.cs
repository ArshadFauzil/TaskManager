namespace TaskManger.Contracts.Task;

public record UpdateTaskRequest(
    string Title,
    string Description,
    DateTime DueDate
);