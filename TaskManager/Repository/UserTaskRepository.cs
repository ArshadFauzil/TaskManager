using Microsoft.EntityFrameworkCore;
using TaskManager.Constants;
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

    public async Task<List<UserTaskDataModel>> getAllUserTasks(int pageNumber)
    {
        return await _dbContext.UserTasks
            .Skip((pageNumber - 1) * AppConstants.PAGINATION_PAGE_SIZE)
            .Take(AppConstants.PAGINATION_PAGE_SIZE)
            .ToListAsync();
    }

    public int getUserTaskCount()
    {
        return _dbContext.UserTasks.Count();
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

    public void DeleteTaskDependentResources(Guid taskId)
    {
        List<UserTaskCommentDataModel> userTaskRelatedComments = _dbContext.Comments
            .Where(c => c.TaskId == taskId)
            .ToList();

        List<UserTaskFileDataModel> userTaskRelatedFiles = _dbContext.Files
            .Where(c => c.TaskId == taskId)
            .ToList();

        userTaskRelatedComments.ForEach(comment => _dbContext.Comments.Remove(comment));
        userTaskRelatedFiles.ForEach(file => _dbContext.Files.Remove(file));

        _dbContext.SaveChanges();
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

    public UserTaskCommentDataModel GetUserTaskCommentById(Guid id)
    {
        if (_dbContext.Comments.Find(id) is UserTaskCommentDataModel userTaskCommentDataModel)
        {
            return userTaskCommentDataModel;
        }
        else
        {
            return null;
        }
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


    //FILES

    public async Task<Guid> CreateUserTaskFile(UserTaskFileDataModel userTaskFileToCreate)
    {
        _dbContext.Files.Add(userTaskFileToCreate);
        await _dbContext.SaveChangesAsync();

        return userTaskFileToCreate.Id;
    }

    public async Task<List<UserTaskFileDataModel>> getUserTaskFilesByTaskId(Guid taskId)
    {
        return await _dbContext.Files
            .Where(c => c.TaskId == taskId)
            .ToListAsync();
    }

    public UserTaskFileDataModel getUserTaskFileById(Guid id)
    {
        if (_dbContext.Files.Find(id) is UserTaskFileDataModel userTaskFile)
        {
            return userTaskFile;
        }
        else
        {
            return null;
        }
    }

    public async Task DeleteUserTaskFile(UserTaskFileDataModel userTaskFileToDelete)
    {
        _dbContext.Files.Remove(userTaskFileToDelete);
        await _dbContext.SaveChangesAsync();
    }
}