using System;
using TaskManager.DataModels;

namespace TaskManager.Repository;

public interface IUserTaskRepository
{
    public Guid CreateUserTask(UserTaskDataModel userTaskToCreate);
    public UserTaskDataModel GetUserTaskById(Guid id);
    public Task<List<UserTaskDataModel>> getAllUserTasks(int pageNumber);
    public Boolean DoesUserTaskExist(Guid id);
    public void UpdateUserTask(UserTaskDataModel userTaskToUpdate);
    public void DeleteUserTask(UserTaskDataModel userTaskToDelete);
    public void DeleteTaskDependentResources(Guid taskId);
    public Guid CreateUserTaskComment(UserTaskCommentDataModel userTaskCommentToCreate);
    public UserTaskCommentDataModel GetUserTaskCommentByIdWithoutTracking(Guid id);
    public List<UserTaskCommentDataModel> GetUserTaskCommentsByTaskId(Guid taskId);
    public void UpdateUserTaskComment(UserTaskCommentDataModel userTaskCommentToUpdate);
    public void DeleteUserTaskComment(UserTaskCommentDataModel userTaskCommentToUpdate);
    public Task<Guid> CreateUserTaskFile(UserTaskFileDataModel userTaskFileToCreate);
    public Task<List<UserTaskFileDataModel>> getUserTaskFilesByTaskId(Guid taskId);
    public UserTaskFileDataModel getUserTaskFileById(Guid id);
    public Task DeleteUserTaskFile(UserTaskFileDataModel userTaskFileToDelete);

}