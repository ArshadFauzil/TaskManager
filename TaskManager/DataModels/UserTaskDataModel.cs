using System.ComponentModel.DataAnnotations;
using TaskManager.Enumerations;

namespace TaskManager.DataModels;

public class UserTaskDataModel
{
    [Key]
    public Guid Id { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public DateTime DueDate { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime LastUpdatedDate { get; set; }
}