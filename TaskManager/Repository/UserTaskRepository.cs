using Microsoft.EntityFrameworkCore;
using TaskManager.DataModels;
using TaskManager.Persistence;

namespace TaskManager.Repository;

public class UserTaskRepository : IUserTaskRepository
{
    private readonly TaskManagerDbContext _dbContext;

    public UserTaskRepository(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid CreateUserTask(UserTaskDataModel userTaskToCreate)
    {
        _dbContext.UserTasks.Add(userTaskToCreate);
        _dbContext.SaveChanges();

        return userTaskToCreate.Id;
    }

    // Apply pagination to this
    public List<UserTaskDataModel> getAllUserTasks()
    {
        return _dbContext.UserTasks.ToList();
    }

    public UserTaskDataModel GetUserTaskById(Guid id)
    {
        if (_dbContext.UserTasks.Find(id) is UserTaskDataModel userTaskDataModel)
        {
            return userTaskDataModel;
        }
        else
        {
            return null;
        }
    }

    public Boolean DoesUserTaskExist(Guid id)
    {
        return _dbContext.UserTasks.Any(t => t.Id == id);
    }

    public void UpdateUserTask(UserTaskDataModel userTaskToUpdate)
    {
        _dbContext.UserTasks.Update(userTaskToUpdate);
        _dbContext.SaveChanges();

    }

    public void DeleteUserTask(UserTaskDataModel userTaskToDelete)
    {
        _dbContext.UserTasks.Remove(userTaskToDelete);
        _dbContext.SaveChanges();
    }


    //COMMENTS

    public Guid CreateUserTaskComment(UserTaskCommentDataModel userTaskCommentToCreate)
    {
        _dbContext.Comments.Add(userTaskCommentToCreate);
        _dbContext.SaveChanges();

        return userTaskCommentToCreate.Id;
    }

    public UserTaskCommentDataModel GetUserTaskCommentByIdWithoutTracking(Guid id)
    {
        if (_dbContext.Comments.AsNoTracking().FirstOrDefault(c => c.Id == id) is
            UserTaskCommentDataModel userTaskCommentDataModel)
        {
            return userTaskCommentDataModel;
        }
        else
        {
            return null;
        }
    }

    public List<UserTaskCommentDataModel> GetUserTaskCommentsByTaskId(Guid taskId)
    {
        return _dbContext.Comments
            .Where(c => c.TaskId == taskId)
            .ToList();
    }

    public void UpdateUserTaskComment(UserTaskCommentDataModel userTaskCommentToUpdate)
    {
        _dbContext.Comments.Update(userTaskCommentToUpdate);
        _dbContext.SaveChanges();

    }

    public void DeleteUserTaskComment(UserTaskCommentDataModel userTaskCommentToUpdate)
    {
        _dbContext.Comments.Remove(userTaskCommentToUpdate);
        _dbContext.SaveChanges();
    }
}