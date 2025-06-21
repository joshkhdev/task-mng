using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Utils.Extensions;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using (var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<TaskManagerDbContext>())
        {
            context.Database.Migrate();
        }
        
        return app;
    }
}