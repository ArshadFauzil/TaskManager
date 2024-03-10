
using System;

namespace TaskManager.Repository;

public interface IUserTaskRepository
{
    public List<UserTaskDataModel> getAllUserTasks();
    public UserTaskDataModel getUserTaskById(Guid id);
}