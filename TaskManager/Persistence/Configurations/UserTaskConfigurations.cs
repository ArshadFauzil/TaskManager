using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DataModels;

namespace TaskManager.Persistence.Configurations
{
    public class UserTaskConfigurations : IEntityTypeConfiguration<UserTaskDataModel>
    {
        public void Configure(EntityTypeBuilder<UserTaskDataModel> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .HasMaxLength(UserTaskDataModel.MaxTitleLength);

            builder.Property(e => e.Description)
                .HasMaxLength(UserTaskDataModel.MaxDescriptionLength);
        }
    }
}