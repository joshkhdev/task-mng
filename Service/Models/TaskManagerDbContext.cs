using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

public class TaskManagerDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<TaskEntity> Tasks { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(u => u.Login);
            user.HasIndex(u => u.Login);

            user.Property(u => u.Login);
            user.Property(u => u.Name);

            user.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Hash);
                password.Property(p => p.Salt);
            });
        });

        modelBuilder.Entity<TaskEntity>(task =>
        {
            task.HasKey(t => t.Id);
            task.HasIndex(t => t.Id);

            task.Property(t => t.Id);
            task.Property(t => t.Name);
            task.Property(t => t.Description);
            task.Property(t => t.CreationDate)
                .HasColumnType("INTEGER");
            task.Property(t => t.CompleteDate)
                .HasColumnType("INTEGER");
            task.Property(t => t.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<TaskEntityStatus>(v));
            task.Property(t => t.PlannedCompletionDate)
                .HasColumnType("INTEGER");
            task.Property(t => t.ActualTimeSpent)
                .HasColumnType("INTEGER");

            task.Property(task => task.UserLogin);

            task.HasOne(task => task.User)
                .WithMany()
                .HasForeignKey(task => task.UserLogin);

            task.HasMany(t => t.Comments)
                .WithOne()
                .HasForeignKey(c => c.TaskId);
        });

        modelBuilder.Entity<Comment>(comment =>
        {
            comment.HasKey(c => c.Id);
            comment.HasIndex(c => c.Id);

            comment.Property(c => c.Id);
            comment.Property(c => c.TaskId);
            comment.Property(c => c.Text);
            comment.Property(c => c.UserLogin);

            comment.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserLogin);
        });
    }
}
