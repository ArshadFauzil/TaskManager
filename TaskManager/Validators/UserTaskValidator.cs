using ErrorOr;
using TaskManager.Contracts.Task;
using TaskManager.DataModels;
using TaskManager.Enumerations;
using TaskManager.ServiceErrors;

namespace TaskManager.Validators;

public class UserTaskValidator : IUserTaskValidator
{
    public List<Error> validateUserTaskCreateRequest(CreateTaskRequest request)
    {
        List<Error> errors = new();
        string title = request.Title;
        string description = request.Description;
        DateTime? dueDate = request.DueDate;

        if (title is null || description is null || dueDate is null)
        {
            errors.Add(Errors.Tasks.MissingFieldsTaskCreateRequest);
        }

        List<Error> commonErrors = ValidateUserTaskCreateAndUpdateRequest(title, description, dueDate);
        errors.AddRange(commonErrors);

        return errors;
    }

    public List<Error> validateUserTaskUpdateRequest(UpdateTaskRequest request)
    {
        List<Error> errors = new();
        string title = request.Title;
        string description = request.Description;
        DateTime? dueDate = request.DueDate;
        string status = request.Status;

        if (title is null || description is null || dueDate is null || status is null)
        {
            errors.Add(Errors.Tasks.MissingFieldsTaskUpdateRequest);
        }

        errors.AddRange(ValidateUserTaskCreateAndUpdateRequest(title, description, dueDate));

        if (status is not null && !Enum.IsDefined(typeof(StatusEnum), status))
        {
            errors.Add(Errors.Tasks.InvalidStatusValue);
        }

        return errors;
    }

    public List<Error> ValidateUserTaskCommentCreateRequest(CreateTaskCommentRequest request)
    {
        List<Error> errors = new();
        Guid? taskId = request.TaskId;
        string comment = request.Comment;

        if (taskId is null || comment is null)
        {
            errors.Add(Errors.Comments.MissingFieldsCommentCreateRequest);
        }

        errors.AddRange(ValidateUserTaskCommentCreateAndUpdateRequest(comment));

        return errors;
    }

    public List<Error> ValidateUserTaskCommentUpdateRequest(UpdateTaskCommentRequest request)
    {
        List<Error> errors = new();
        string comment = request.Comment;

        if (comment is null)
        {
            errors.Add(Errors.Comments.MissingFieldsCommentUpdateRequest);
        }

        errors.AddRange(ValidateUserTaskCommentCreateAndUpdateRequest(comment));

        return errors;
    }

    public List<Error> ValidateUserTaskFileCreateRequest(CreateTaskFileRequest request)
    {
        List<Error> errors = new();
        Guid? taskId = request.TaskId;
        IFormFile? file = request.File;
        string? fileType = request.FileType;

        if (taskId is null || file is null || fileType is null)
        {
            errors.Add(Errors.Files.MissingFieldsFileCreateRequest);
        }

        return errors;
    }

    private List<Error> ValidateUserTaskCreateAndUpdateRequest(string title, string description, DateTime? dueDate)
    {
        List<Error> errors = new();

        if (title is not null && title.Length is < UserTaskDataModel.MinTitleLength
                or > UserTaskDataModel.MaxTitleLength)
        {
            errors.Add(Errors.Tasks.InvalidTitle);
        }

        if (description is not null && description.Length is < UserTaskDataModel.MinDescriptionLength
                or > UserTaskDataModel.MaxDescriptionLength)
        {
            errors.Add(Errors.Tasks.InvalidDescription);
        }

        if (dueDate is not null && dueDate < DateTime.Now)
        {
            errors.Add(Errors.Tasks.InvalidDueDate);
        }

        return errors;
    }

    private List<Error> ValidateUserTaskCommentCreateAndUpdateRequest(string comment)
    {
        List<Error> errors = new();

        if (comment is not null && comment.Length is < UserTaskCommentDataModel.MinCommentLength
                or > UserTaskCommentDataModel.MaxCommentLength)
        {
            errors.Add(Errors.Comments.InvalidComment);
        }

        return errors;
    }
}