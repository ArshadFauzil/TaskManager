namespace TaskManager.Contracts.Task;

public record CreateTaskRequest(
    string Title,
    string Description,
    DateTime? DueDate
);