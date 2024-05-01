using Application;
using Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.MsSqlEFCore;

namespace AcceptanceTests
{
    public abstract class ServiceContext
    {
        public ServiceProvider ServiceProvider { get; set; }
        public ServiceContext()
        {
            var services = new ServiceCollection();

            var databaseSettings = new DatabaseSettings
            {
                IsInMemory = true
            };

            var inMemoryDatabaseSettings = new InMemoryDatabaseSettings
            {
                DatabaseName = Guid.NewGuid().ToString()
            };

            services.ConfigureApplicationServices(
                databaseSettings: databaseSettings,
                inMemoryDatabaseSettings: inMemoryDatabaseSettings);

            ServiceProvider = services.BuildServiceProvider(validateScopes: true);

            EnsureRecreatedDatabase();
        }

        public void EnsureRecreatedDatabase()
        {
            var serviceScope = ServiceProvider.CreateAsyncScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            if (ServiceProvider != null)
                ServiceProvider.Dispose();
        }
    }
}