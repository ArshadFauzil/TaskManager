namespace TaskManager.Contracts.Task;

public record TaskFileResponseFileContract(
    Guid Id,
    StreamContent file,
    string fileName,
    string fileType,
    DateTime LastUpdatedDate
);