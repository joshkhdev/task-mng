using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

public class TaskManagerDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(e => e.Login);
            user.HasIndex(e => e.Login);

            user.Property(e => e.Login);
            user.Property(e => e.Name);

            user.OwnsOne(e => e.Password, password =>
            {
                password.Property(p => p.Hash);
                password.Property(p => p.Salt);
            });
        });
    }
}
