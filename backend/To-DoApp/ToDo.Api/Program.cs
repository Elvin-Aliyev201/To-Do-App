
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ToDo.Api.Data;
using ToDo.Api.Services;

namespace ToDo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();
            var builder = WebApplication.CreateBuilder(args);
            var databaseHost = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
            var databasePort = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
            var databaseName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "TodoDb";

            var databaseUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            var databasePassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

            if (string.IsNullOrWhiteSpace(databaseUser))
            {
                throw new InvalidOperationException("POSTGRES_USER environment variable is missing.");
            }

            if (string.IsNullOrWhiteSpace(databasePassword))
            {
                throw new InvalidOperationException("POSTGRES_PASSWORD environment variable is missing.");
            }

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseHost,
                Port = int.Parse(databasePort),
                Database = databaseName,
                Username = databaseUser,
                Password = databasePassword,
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            var connectionString = connectionStringBuilder.ConnectionString;

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL")
                ?? throw new InvalidOperationException("FRONTEND_URL environment variable is missing.");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:5173",
                            "https://to-do-app-sigma-eight-17.vercel.app"
                          )
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddScoped<ITodoService, TodoService>();

            builder.Services.AddControllers();
       
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

          
     
                app.UseSwagger();
                app.UseSwaggerUI(); 
         
            

            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthorization();

          
            app.MapControllers();

            app.Run();
        }
    }
}
