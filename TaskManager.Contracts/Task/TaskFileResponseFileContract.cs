namespace TaskManager.Contracts.Task;

public record TaskFileResponseFileContract(
    Guid Id,
    ByteArrayContent file,
    string fileName,
    string fileType,
    DateTime LastUpdatedDate
);