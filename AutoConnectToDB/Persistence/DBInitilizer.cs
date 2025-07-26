using Microsoft.EntityFrameworkCore;

namespace AutoConnectToDB.Persistence
{
    public interface IDatabaseInitializer
    {
        Task InitializeDatabasesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
    internal class DatabaseInitializer(IServiceProvider serviceProvider) : IDatabaseInitializer
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
                .InitializeAsync(cancellationToken);
        }
    }
    internal class ApplicationDbInitializer(ContextForDB dbContext, ILogger<ApplicationDbInitializer> logger)
    {
        private readonly ContextForDB _dbContext = dbContext;
        private readonly ILogger<ApplicationDbInitializer> _logger = logger;
        //private readonly ApplicationDbSeeder _dbSeeder = dbSeeder;
        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (_dbContext.Database.GetMigrations().Any())
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    _logger.LogInformation("Applying Migrations");
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }
                if (await _dbContext.Database.CanConnectAsync(cancellationToken))
                {
                    _logger.LogInformation("Connection to Database Succeeded.");

                    //await _dbSeeder.SeedDatabaseAsync(cancellationToken);
                }
            }
        }
    }

}
