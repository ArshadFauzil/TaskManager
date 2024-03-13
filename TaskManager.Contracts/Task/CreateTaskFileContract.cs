using Microsoft.AspNetCore.Http;
namespace TaskManager.Contracts.Task;

public record CreateTaskFileContract(
    IFormFile? File,
    string? FileType
);