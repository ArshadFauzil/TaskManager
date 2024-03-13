using ErrorOr;
using TaskManager.Contracts.Task;
using TaskManager.DataModels;
using TaskManager.Enumerations;
using TaskManager.Repository;
using TaskManager.ServiceErrors;
using TaskManager.Validators;

namespace TaskManager.Services.Tasks;

public class TaskService : ITaskService
{
    private readonly IUserTaskRepository _userTaskRepository;
    private readonly IUserTaskValidator _userTaskValidator;

    public TaskService(IUserTaskRepository userTaskRepository,
                        IUserTaskValidator userTaskValidator)
    {
        _userTaskRepository = userTaskRepository;
        _userTaskValidator = userTaskValidator;
    }

    public ErrorOr<Guid> createTask(CreateTaskRequest request)
    {
        List<Error> errors = _userTaskValidator.validateUserTaskCreateRequest(request);

        if (errors.Any())
        {
            return errors;
        }

        UserTaskDataModel userTaskToCreate = MapUserTaskCreateRequestToDataModel(request);
        Guid newId = _userTaskRepository.CreateUserTask(userTaskToCreate);
        return newId;

    }

    public ErrorOr<TaskResponse> getTask(Guid id)
    {
        UserTaskDataModel response = _userTaskRepository.GetUserTaskById(id);

        if (response is not null)
        {
            return MapUserTaskDataModelToResponse(response);
        }
        else
        {
            return Errors.Tasks.NotFound;
        }
    }

    public ErrorOr<Updated> UpdateTask(Guid id, UpdateTaskRequest request)
    {
        bool userTaskExists = _userTaskRepository.DoesUserTaskExist(id);
        if (!userTaskExists)
        {
            return Errors.Tasks.NotFoundValidation;
        }

        List<Error> errors = _userTaskValidator.validateUserTaskUpdateRequest(request);

        if (errors.Any())
        {
            return errors;
        }

        UserTaskDataModel userTaskToUpdate = MapUserTaskUpdateRequestToDataModel(id, request);
        _userTaskRepository.UpdateUserTask(userTaskToUpdate);

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteTask(Guid id)
    {
        UserTaskDataModel userTaskToDelete = _userTaskRepository.GetUserTaskById(id);
        if (userTaskToDelete is null)
        {
            return Errors.Tasks.NotFoundValidation;
        }

        _userTaskRepository.DeleteUserTask(userTaskToDelete);

        return Result.Deleted;
    }


    //COMMENTS

    public ErrorOr<Guid> createTaskComment(CreateTaskCommentRequest request)
    {
        List<Error> errors = _userTaskValidator.ValidateUserTaskCommentCreateRequest(request);

        if (errors.Any())
        {
            return errors;
        }

        UserTaskCommentDataModel userTaskCommentToCreate = MapUserTaskCommentCreateRequestToDataModel(request);
        Guid newId = _userTaskRepository.CreateUserTaskComment(userTaskCommentToCreate);
        return newId;
    }

    public ErrorOr<List<TaskCommentResponse>> getUserTaskCommentsByTaskId(Guid taskId)
    {
        bool userTaskExists = _userTaskRepository.DoesUserTaskExist(taskId);
        if (!userTaskExists)
        {
            return Errors.Tasks.NotFoundValidation;
        }

        return MapUserTaskDataCommentModelToResponse(_userTaskRepository.GetUserTaskCommentsByTaskId(taskId));

    }

    public ErrorOr<Updated> UpdateTaskComment(Guid id, UpdateTaskCommentRequest request)
    {
        UserTaskCommentDataModel persistedComment = _userTaskRepository.GetUserTaskCommentByIdWithoutTracking(id);
        if (persistedComment is null)
        {
            return Errors.Comments.NotFoundValidation;
        }

        List<Error> errors = _userTaskValidator.ValidateUserTaskCommentUpdateRequest(request);

        if (errors.Any())
        {
            return errors;
        }

        UserTaskCommentDataModel userTaskCommentToUpdate =
            MapUserTaskCommentUpdateRequestToDataModel(id, persistedComment.TaskId, request);

        _userTaskRepository.UpdateUserTaskComment(userTaskCommentToUpdate);

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteTaskComment(Guid id)
    {
        UserTaskCommentDataModel persistedComment = _userTaskRepository.GetUserTaskCommentByIdWithoutTracking(id);
        if (persistedComment is null)
        {
            return Errors.Comments.NotFoundValidation;
        }

        _userTaskRepository.DeleteUserTaskComment(persistedComment);

        return Result.Deleted;
    }



    public UserTaskDataModel MapUserTaskCreateRequestToDataModel(CreateTaskRequest request)
    {
        // status is set to INCOMPLETE at the point of creation
        return new UserTaskDataModel(
            request.Title,
            request.Description,
            request.DueDate.Value,
            StatusEnum.INCOMPLETE,
            DateTime.Now
        );
    }

    public UserTaskDataModel MapUserTaskUpdateRequestToDataModel(Guid id, UpdateTaskRequest request)
    {
        Enum.TryParse(request.Status, out StatusEnum statusToUpdate);
        UserTaskDataModel dataModel = new UserTaskDataModel(
            id,
            request.Title,
            request.Description,
            request.DueDate.Value,
            statusToUpdate,
            DateTime.Now
        );

        return dataModel;
    }

    public UserTaskCommentDataModel MapUserTaskCommentCreateRequestToDataModel(CreateTaskCommentRequest request)
    {
        return new UserTaskCommentDataModel(
            request.TaskId,
            request.Comment,
            DateTime.Now
        );
    }

    public UserTaskCommentDataModel MapUserTaskCommentUpdateRequestToDataModel(Guid Id, Guid TaskId, UpdateTaskCommentRequest request)
    {
        return new UserTaskCommentDataModel(
            Id,
            TaskId,
            request.Comment,
            DateTime.Now
        );
    }

    public TaskResponse MapUserTaskDataModelToResponse(UserTaskDataModel model)
    {
        return new TaskResponse(
            model.Id,
            model.Title,
            model.Description,
            model.DueDate,
            model.Status.ToString(),
            model.LastUpdatedDate
        );
    }

    public List<TaskCommentResponse> MapUserTaskDataCommentModelToResponse(List<UserTaskCommentDataModel> comments)
    {
        List<TaskCommentResponse> responseList = new();
        comments.ForEach(c =>
        {
            responseList.Add(new TaskCommentResponse(
                c.Id,
                c.TaskId,
                c.Comment,
                c.LastUpdatedDate
            ));
        });

        return responseList;
    }

}