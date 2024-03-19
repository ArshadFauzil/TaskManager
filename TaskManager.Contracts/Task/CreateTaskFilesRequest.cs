using Microsoft.AspNetCore.Http;
namespace TaskManager.Contracts.Task;

public record CreateTaskFileRequest(
    Guid? TaskId,
    IFormFile? File,
    string? FileType
);