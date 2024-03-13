using System;
using TaskManager.DataModels;

namespace TaskManager.Repository;

public interface IUserTaskRepository
{
    public Guid CreateUserTask(UserTaskDataModel userTaskToCreate);
    public UserTaskDataModel GetUserTaskById(Guid id);
    public Boolean DoesUserTaskExist(Guid id);
    public void UpdateUserTask(UserTaskDataModel userTaskToUpdate);
    public void DeleteUserTask(UserTaskDataModel userTaskToDelete);
    public Guid CreateUserTaskComment(UserTaskCommentDataModel userTaskCommentToCreate);
    public UserTaskCommentDataModel GetUserTaskCommentByIdWithoutTracking(Guid id);
    public List<UserTaskCommentDataModel> GetUserTaskCommentsByTaskId(Guid taskId);
    public void UpdateUserTaskComment(UserTaskCommentDataModel userTaskCommentToUpdate);
    public void DeleteUserTaskComment(UserTaskCommentDataModel userTaskCommentToUpdate);
}