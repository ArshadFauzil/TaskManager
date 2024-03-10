using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TaskManager.Models;
using TaskManger.Contracts.Task;

namespace TaskManager.Services.Tasks;

public interface ITaskService
{
    public void createTask(UserTask newTask);
    public UserTask getTask(Guid id);
    public void UpdateTask(UserTask task);
    public void DeleteTask(Guid id);
    
}