namespace TaskManager.Contracts.Task;

public record UpdateTaskRequest(
    string? Title,
    string? Description,
    DateTime? DueDate,
    string? Status
);