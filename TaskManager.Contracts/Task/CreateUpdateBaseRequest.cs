namespace TaskManager.Contracts.Task;

public record CreateUpdateBaseRequest(
    string Title,
    string Description,
    DateTime? DueDate
);