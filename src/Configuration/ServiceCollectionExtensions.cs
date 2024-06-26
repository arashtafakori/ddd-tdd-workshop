﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application;

namespace Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            var databaseSettings = new DatabaseSettings();
            configuration.GetSection("DatabaseSettings").Bind(databaseSettings);

            var inMemoryDatabaseSettings = new InMemoryDatabaseSettings();
            configuration.GetSection("InMemoryDatabaseSettings").Bind(inMemoryDatabaseSettings);

            var sqlServerSettings = new SqlServerSettings();
            configuration.GetSection("SqlServerSettings").Bind(sqlServerSettings);

            services.ConfigureApplicationServices(
                databaseSettings: databaseSettings,
                inMemoryDatabaseSettings: inMemoryDatabaseSettings,
                sqlServerSettings: sqlServerSettings);
        }

        public static void ConfigureApplicationServices(
            this IServiceCollection services,
            DatabaseSettings databaseSettings,
            SqlServerSettings? sqlServerSettings = null,
            InMemoryDatabaseSettings? inMemoryDatabaseSettings = null)
        {
            services.AddApplicationServices(
                databaseSettings,
                sqlServerSettings,
                inMemoryDatabaseSettings);
        }
    }
}
