using BookARoom.Data;
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
        services.AddDbContext<BookARoomContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresqlDatabase")));
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
}