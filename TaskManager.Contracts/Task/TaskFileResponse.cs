namespace TaskManager.Contracts.Task;

public record TaskFileResponse(
    Guid Id,
    Guid TaskId,
    ByteArrayContent file,
    string fileName,
    string fileType,
    DateTime LastUpdatedDate
);