using BookARoom.Extensions;
using BookARoom.Utilities;

namespace BookARoom;

public class Program
{
    public static void BookARoom(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.ConfigureDatabaseContext(builder.Configuration);
        builder.Services.ConfigureRepositoryManager();
        builder.Services.ConfigureServiceManager();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.ConfigureRedis(builder.Configuration);


        // Add Dto Validation
        builder.Services.AddScoped<ValidateDtoFilter>();

        var app = builder.Build();

        Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

        // add exception handler
        app.ConfigureExceptionHandler();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else if (app.Environment.IsProduction())
        {

            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }

    static void Main(string[] args)
    {
        BookARoom(args);
    }
}
