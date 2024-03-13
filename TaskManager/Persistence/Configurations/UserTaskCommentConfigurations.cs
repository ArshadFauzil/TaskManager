using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DataModels;

namespace TaskManager.Persistence.Configurations
{
    public class UserTaskCommentConfigurations : IEntityTypeConfiguration<UserTaskCommentDataModel>
    {
        public void Configure(EntityTypeBuilder<UserTaskCommentDataModel> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Comment)
                .HasMaxLength(UserTaskCommentDataModel.MaxCommentLength);
        }
    }
}