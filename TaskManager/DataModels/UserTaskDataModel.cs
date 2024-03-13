using TaskManager.Enumerations;

namespace TaskManager.DataModels;

public class UserTaskDataModel
{
    public const int MinTitleLength = 3;
    public const int MaxTitleLength = 50;

    public const int MinDescriptionLength = 5;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    // The Status is defined as an Enum instead of a boolean if other statuses come in the future
    // Although currently only two statuses are required
    public StatusEnum Status { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public List<UserTaskCommentDataModel>? Comments { get; set; }

    public UserTaskDataModel(
        string title,
        string description,
        DateTime dueDate,
        StatusEnum status,
        DateTime lastUpdatedDate
    )
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = status;
        LastUpdatedDate = lastUpdatedDate;
    }

    public UserTaskDataModel(
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