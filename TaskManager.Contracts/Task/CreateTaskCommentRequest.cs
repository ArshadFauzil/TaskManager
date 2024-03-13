namespace TaskManager.Contracts.Task;

public record CreateTaskCommentRequest(
    Guid? TaskId,
    string? Comment
);