using Microsoft.EntityFrameworkCore;
using TaskManager.DataModels;

namespace TaskManager.Persistence;

public class TaskManagerDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TaskManager.db");
    }

    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    public DbSet<UserTaskDataModel> UserTasks { get; set; }
    public DbSet<UserTaskCommentDataModel> Comments { get; set; }
    public DbSet<UserTaskFileDataModel> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerDbContext).Assembly);
    }
}