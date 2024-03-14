using ErrorOr;
using Microsoft.Extensions.Caching.Memory;
using TaskManager.Constants;
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
    private readonly ILogger<TaskService> _logger;
    private readonly IMemoryCache _cache;
    private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    private static List<string> cachedUserTasksPageNumbers = new();


    public TaskService(IUserTaskRepository userTaskRepository,
                        IUserTaskValidator userTaskValidator,
                        IMemoryCache memoryCache,
                        ILogger<TaskService> logger)
    {
        _userTaskRepository = userTaskRepository;
        _userTaskValidator = userTaskValidator;
        _logger = logger;
        _cache = memoryCache;
    }

    public ErrorOr<Guid> createTask(CreateTaskRequest request)
    {
        List<Error> errors = _userTaskValidator.validateUserTaskCreateRequest(request);

        if (errors.Count > 0)
        {
            return errors;
        }

        UserTaskDataModel userTaskToCreate = MapUserTaskCreateRequestToDataModel(request);
        Guid newId = _userTaskRepository.CreateUserTask(userTaskToCreate);

        emptyUserTasksCache();

        return newId;

    }

    public async Task<ErrorOr<List<TaskResponse>>> getAllUserTasks(int pageNumber)
    {
        string cacheKey = pageNumber.ToString();
        List<TaskResponse>? userTasks = (List<TaskResponse>)CacheExtensions.Get(_cache, cacheKey);
        if (userTasks is not null)
        {
            _logger.LogInformation("User Tasks list returned from cache.");
        }
        else
        {
            _logger.LogInformation("User Tasks not found in cache. Fetching from Database.");
            try
            {
                await semaphore.WaitAsync();
                List<UserTaskDataModel> userTaskDataModels = await _userTaskRepository.getAllUserTasks(pageNumber);
                userTasks = MapUserTaskDataModelListToResponse(userTaskDataModels);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(AppConstants.CACHING_SLIDING_EXPIRATION_SECONDS))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(AppConstants.CACHING_ABSOLUTE_EXPIRATION_SECONDS))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(AppConstants.CACHING_MEMORY_SIZE);

                if (userTasks.Count > 0)
                {
                    _logger.LogInformation("Caching page number {PN}", pageNumber);

                    _cache.Set(cacheKey, userTasks, cacheEntryOptions);

                    cachedUserTasksPageNumbers.Add(cacheKey);
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        return userTasks;
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

        emptyUserTasksCache();

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

        _logger.LogWarning("Deleting all User Task dependent resources such as Comments and Files.");

        _userTaskRepository.DeleteTaskDependentResources(id);

        emptyUserTasksCache();

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

        bool userTaskExists = _userTaskRepository.DoesUserTaskExist(request.TaskId.Value);
        if (!userTaskExists)
        {
            return Errors.Tasks.NotFoundValidation;
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


    // FILES

    public async Task<ErrorOr<List<Guid>>> CreateUserTaskFile(CreateTaskFilesRequest request)
    {
        List<Error> errors = _userTaskValidator.ValidateUserTaskFileCreateRequest(request);
        if (errors.Any())
        {
            return errors;
        }

        try
        {
            List<UserTaskFileDataModel> dataModels = MapUserTaskFileCreateRequestToDataModel(request);

            List<Guid> fileIds = new();
            foreach (UserTaskFileDataModel dataModel in dataModels)
            {
                Guid fileId = await _userTaskRepository.CreateUserTaskFile(dataModel);
                fileIds.Add(fileId);
            }

            return fileIds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<ErrorOr<TaskFileResponse>> getUserTaskFilesByTaskId(Guid taskId)
    {
        bool userTaskExists = _userTaskRepository.DoesUserTaskExist(taskId);
        if (!userTaskExists)
        {
            return Errors.Tasks.NotFoundValidation;
        }

        List<UserTaskFileDataModel> fileDataModels = await _userTaskRepository.getUserTaskFilesByTaskId(taskId);

        return MapUserTaskDataFileModelToResponse(taskId, fileDataModels);
    }

    public ErrorOr<Deleted> DeleteTaskFile(Guid id)
    {
        UserTaskFileDataModel persistedFile = _userTaskRepository.getUserTaskFileById(id);
        if (persistedFile is null)
        {
            return Errors.Files.NotFoundValidation;
        }

        _userTaskRepository.DeleteUserTaskFile(persistedFile);

        return Result.Deleted;
    }

    private void emptyUserTasksCache()
    {
        cachedUserTasksPageNumbers.ForEach(pn =>
            _cache.Remove(pn)
        );

        cachedUserTasksPageNumbers.Clear();
    }

    private UserTaskDataModel MapUserTaskCreateRequestToDataModel(CreateTaskRequest request)
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

    private UserTaskDataModel MapUserTaskUpdateRequestToDataModel(Guid id, UpdateTaskRequest request)
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

    private UserTaskCommentDataModel MapUserTaskCommentCreateRequestToDataModel(CreateTaskCommentRequest request)
    {
        return new UserTaskCommentDataModel(
            request.TaskId.Value,
            request.Comment,
            DateTime.Now
        );
    }

    private UserTaskCommentDataModel MapUserTaskCommentUpdateRequestToDataModel(Guid Id, Guid TaskId, UpdateTaskCommentRequest request)
    {
        return new UserTaskCommentDataModel(
            Id,
            TaskId,
            request.Comment,
            DateTime.Now
        );
    }

    private List<UserTaskFileDataModel> MapUserTaskFileCreateRequestToDataModel(CreateTaskFilesRequest request)
    {
        List<UserTaskFileDataModel> dataModels = new();
        List<CreateTaskFileContract>? files = request.Files;

        files.ForEach(fileContract =>
        {
            var stream = new MemoryStream();
            fileContract.File.CopyTo(stream);

            dataModels.Add(
                new UserTaskFileDataModel(
                    request.TaskId.Value,
                    fileContract.File.FileName,
                    fileContract.FileType,
                    stream.ToArray(),
                    DateTime.Now
                )
            );
        });

        return dataModels;
    }

    private List<TaskResponse> MapUserTaskDataModelListToResponse(List<UserTaskDataModel> userTaskDataModels)
    {
        List<TaskResponse> responses = new();
        userTaskDataModels.ForEach(userTaskDataModel =>
            responses.Add(MapUserTaskDataModelToResponse(userTaskDataModel)));

        return responses;
    }

    private TaskResponse MapUserTaskDataModelToResponse(UserTaskDataModel model)
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

    private List<TaskCommentResponse> MapUserTaskDataCommentModelToResponse(List<UserTaskCommentDataModel> comments)
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

    private TaskFileResponse MapUserTaskDataFileModelToResponse(Guid taskId, List<UserTaskFileDataModel> files)
    {
        List<TaskFileResponseFileContract> fileList = new();

        foreach (UserTaskFileDataModel file in files)
        {
            var dataStream = new MemoryStream(file.FileData);

            fileList.Add(new TaskFileResponseFileContract(
                file.Id,
                new StreamContent(dataStream),
                file.FileName,
                file.FileType,
                file.LastUpdatedDate
            ));
        }

        return new TaskFileResponse(
            taskId,
            fileList
        );
    }

}