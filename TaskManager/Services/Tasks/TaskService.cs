using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Models;

namespace TaskManager.Services.Tasks;

public class TaskService : ITaskService
{

    private readonly static Dictionary<Guid, UserTask> _userTasks = new();

    public void createTask(UserTask newTask)
    {
        _userTasks.Add(newTask.Id, newTask);
    }

    public UserTask getTask(Guid id)
    {
        return _userTasks[id];
    }

    public void UpdateTask(UserTask task)
    {
        _userTasks[task.Id] = task;
    }

    public void DeleteTask(Guid id)
    {
        _userTasks.Remove(id);
    }


}