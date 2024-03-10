using Microsoft.EntityFrameworkCore;
using TaskManager.DataModels;

namespace TaskManager.DatabaseContext;

public class TaskManagerDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "TaskManagerDb");
    }

    public DbSet<UserTaskDataModel> UserTasks { get; set; }
    public DbSet<UserTaskCommentDataModel> Comments { get; set; }
}