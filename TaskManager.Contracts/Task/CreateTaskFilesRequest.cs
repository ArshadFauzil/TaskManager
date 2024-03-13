namespace TaskManager.Contracts.Task;

public record CreateTaskFilesRequest(
    Guid? TaskId,
    List<CreateTaskFileContract>? Files
);