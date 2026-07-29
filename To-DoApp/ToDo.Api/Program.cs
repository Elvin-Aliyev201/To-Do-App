
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Data;

namespace ToDo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var databaseHost = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
            var databasePort = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
            var databaseName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "TodoDb";
            var databaseUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
            var databasePassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres123";

            var connectionString =
                $"Host={databaseHost};" +
                $"Port={databasePort};" +
                $"Database={databaseName};" +
                $"Username={databaseUser};" +
                $"Password={databasePassword}";

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });



            builder.Services.AddControllers();
       
            builder.Services.AddOpenApi();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
