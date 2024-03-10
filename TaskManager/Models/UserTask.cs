using ErrorOr;
using TaskManager.Enumerations;

namespace TaskManager.Models;

public class UserTask
{
    public Guid Id { get; }
    public string Title { get; }
    public string Description { get; }
    public DateTime DueDate { get; }
    public StatusEnum Status { get; }
    public DateTime LastUpdatedDate { get; }

    public UserTask(
        Guid id,
        string title,
        string description,
        DateTime dueDate,
        StatusEnum status,
        DateTime lastUpdatedDate
    )
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = status;
        LastUpdatedDate = lastUpdatedDate;
    }
}