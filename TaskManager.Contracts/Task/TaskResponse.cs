namespace TaskManager.Contracts.Task;

public record TaskResponse(
    Guid Id,
    string Title,
    string Description,
    DateTime DueDate,
    string Status,
    DateTime LastUpdatedDate,
    int NumberOfPagesOfUserTasks
);