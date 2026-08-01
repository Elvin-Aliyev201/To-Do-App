using Microsoft.EntityFrameworkCore;
using ToDo.Api.Entities;

namespace ToDo.Api.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Todo> Todos => Set<Todo>();

        public DbSet<User> Users => Set<User>();

       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }



    }
}
