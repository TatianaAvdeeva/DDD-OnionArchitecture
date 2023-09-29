using DDDTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Application
{
    public static class MigrationExtension
    {
        public static async Task RunMigrationAsync(this IHost app)
        {
            using var scope = app.Services
                .CreateScope();

            await scope
                .ServiceProvider
                .GetRequiredService<TemplateDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
