using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.DataAccess.ValueConverters;
using ToDo.DomainModel;

namespace ToDo.DataAccess.Configurations
{
    internal class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable(nameof(Task));

            builder
                .HasKey(i => i.Id);

            builder
                .Property(d => d.TaskType)
                .HasColumnType("CHAR (10)")
                .IsRequired();

            builder
                .Property(nameof(Task.TaskType))
                .HasConversion(new TaskTypeConverter());

            builder
                .Property(i => i.Active)
                .HasColumnType("BIT")
                .IsRequired();

            builder
                .Property(i => i.Name)
                .HasColumnType("NVARCHAR(256)")
                .IsRequired();

            builder
                .Property(i => i.Description)
                .HasColumnType("NVARCHAR(MAX)")
                .IsRequired();

            builder
                .Property(i => i.CreatedDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder
                .Property(i => i.UpdatedDate)
                .HasColumnType("DATETIME")
                .IsRequired();
        }
    }
}
