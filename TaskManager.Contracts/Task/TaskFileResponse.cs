namespace TaskManager.Contracts.Task;

public record TaskFileResponse(
    Guid TaskId,
    List<TaskFileResponseFileContract> files
);