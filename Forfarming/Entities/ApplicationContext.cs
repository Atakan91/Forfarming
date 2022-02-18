using Forfarming.Models;
using Microsoft.EntityFrameworkCore;

namespace Forfarming.Entities
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext()
        {

        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) 
        { }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDB");
            optionsBuilder.UseSqlServer(connectionString);

           
        }
        
    }
}
