using BookARoom.Data;
using BookARoom.Interfaces;
using BookARoom.Repository;
using BookARoom.Services;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Extensions;

public static class ServiceExtensions
{

    /// <summary>
    /// Register database context
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string? databaseConnectionString;

        if (environment == "Development")
        {
            databaseConnectionString = configuration.GetConnectionString("PostgresqlDatabase");
        }
        else
        {
            databaseConnectionString = configuration.GetConnectionString("PostgresqlDatabaseStaging");
        }

        // Console.WriteLine($"env ==== {environment}\nconn ==== {databaseConnectionString}");

        services.AddDbContext<BookARoomContext>(options =>
            options.UseNpgsql(
                databaseConnectionString,
                op => op.MigrationsHistoryTable("BookARoomMigrationsHistory")));
    }

    /// <summary>
	/// Configure cors for application
	/// </summary>
	/// <param name="services"></param>
	public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    }

    /// <summary>
    /// Configure a scoped repository manager dependency injection
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    /// <summary>
    /// Configure a scoped service manager dependency injection
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }
}