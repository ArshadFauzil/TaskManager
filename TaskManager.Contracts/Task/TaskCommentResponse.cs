namespace TaskManager.Contracts.Task;

public record TaskCommentResponse(
    Guid Id,
    Guid TaskId,
    string Comment,
    DateTime LastUpdatedDate
);