using Contract;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Persistence.MsSqlEFCore;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        DatabaseSettings databaseSettings,
        SqlServerSettings? sqlServerSettings = null,
        InMemoryDatabaseSettings? inMemoryDatabaseSettings = null)
    {
        services.ConfigureDataStore(
            databaseSettings,
            sqlServerSettings,
            inMemoryDatabaseSettings);

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    private static void ConfigureDataStore(
        this IServiceCollection services,
        DatabaseSettings databaseSettings,
        SqlServerSettings? sqlServerSettings = null,
        InMemoryDatabaseSettings? inMemoryDatabaseSettings = null)
    {
 

        if (databaseSettings.IsInMemory)
        {
            services.AddDbContext<WarehouseDbContext>(options =>
               options.UseInMemoryDatabase(databaseName: inMemoryDatabaseSettings!.DatabaseName)
               .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)),
               ServiceLifetime.Scoped);
        }
        else
        {
            var assembly = typeof(WarehouseDbContext).Assembly.GetName().Name;
            services.AddDbContext<WarehouseDbContext>(options =>
               options.UseSqlServer(
                   sqlServerSettings!.ConnectionString!,
                   b => b.MigrationsAssembly(assembly)),
               ServiceLifetime.Scoped);
        }

        new DatabaseInitialization(services).Initialize();
    }
}