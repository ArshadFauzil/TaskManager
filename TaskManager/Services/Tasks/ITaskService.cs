using ErrorOr;
using TaskManager.Contracts.Task;

namespace TaskManager.Services.Tasks;

public interface ITaskService
{
    public ErrorOr<Guid> createTask(CreateTaskRequest request);
    public ErrorOr<TaskResponse> getTask(Guid id);
    public ErrorOr<Updated> UpdateTask(Guid id, UpdateTaskRequest request);
    public ErrorOr<Deleted> DeleteTask(Guid id);
    public ErrorOr<Guid> createTaskComment(CreateTaskCommentRequest request);
    public ErrorOr<List<TaskCommentResponse>> getUserTaskCommentsByTaskId(Guid taskId);
    public ErrorOr<Updated> UpdateTaskComment(Guid id, UpdateTaskCommentRequest request);
    public ErrorOr<Deleted> DeleteTaskComment(Guid id);

}