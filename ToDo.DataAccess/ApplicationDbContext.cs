using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.ValueConverters;

namespace ToDo.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Task>(b =>
            {
                b.ToTable("Task");

                b.Property(c => c.Type)
                 .HasColumnType("char(10)")
                 .HasConversion(new TypeConverter());
            });
        }
    }
}
