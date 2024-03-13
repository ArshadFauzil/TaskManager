namespace TaskManager.DataModels;

public class UserTaskCommentDataModel
{
    public const int MinCommentLength = 3;
    public const int MaxCommentLength = 500;

    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public string Comment { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public UserTaskCommentDataModel(
        Guid id,
        Guid taskId,
        string comment,
        DateTime lastUpdatedDate
    )
    {
        Id = id;
        TaskId = taskId;
        Comment = comment;
        LastUpdatedDate = lastUpdatedDate;
    }

    public UserTaskCommentDataModel(
        Guid taskId,
        string comment,
        DateTime lastUpdatedDate
    )
    {
        TaskId = taskId;
        Comment = comment;
        LastUpdatedDate = lastUpdatedDate;
    }
}