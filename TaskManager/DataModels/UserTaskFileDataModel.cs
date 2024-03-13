
namespace TaskManager.DataModels;

public class UserTaskFileDataModel
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public byte[] FileData { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public UserTaskFileDataModel(
        Guid taskId,
        string fileName,
        string fileType,
        byte[] fileData,
        DateTime lastUpdatedDate
    )
    {
        TaskId = taskId;
        FileName = fileName;
        FileType = fileType;
        FileData = fileData;
        LastUpdatedDate = lastUpdatedDate;
    }
}