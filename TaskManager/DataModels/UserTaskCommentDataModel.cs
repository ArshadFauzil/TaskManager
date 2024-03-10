namespace TaskManager.DataModels;

public class UserTaskCommentDataModel
{
    public Guid TaskId { get; set; }
    public List<String> comments { get; set; }
    public DateTime LastUpdatedDate { get; set; }
}