using ErrorOr;
using TaskManager.Contracts.Task;

namespace TaskManager.Validators;

public interface IUserTaskValidator
{
    public List<Error> validateUserTaskCreateRequest(CreateTaskRequest request);
    public List<Error> validateUserTaskUpdateRequest(UpdateTaskRequest request);
    public List<Error> ValidateUserTaskCommentCreateRequest(CreateTaskCommentRequest request);
    public List<Error> ValidateUserTaskCommentUpdateRequest(UpdateTaskCommentRequest request);
    public List<Error> ValidateUserTaskFileCreateRequest(CreateTaskFilesRequest request);
}