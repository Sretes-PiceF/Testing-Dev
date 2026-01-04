using DevHabit.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using ApplicationDBContext dBContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

        try
        {
            await dBContext.Database.MigrateAsync();

            app.Logger.LogInformation("Database migrations applied successfully.");
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "An error occurred while applying database migrations.");
            app.Logger.LogWarning("Continuing without applied migrations. Verify database connection settings.");
            // Do not rethrow to allow the application/container to start even if migrations fail
        }
    }
}