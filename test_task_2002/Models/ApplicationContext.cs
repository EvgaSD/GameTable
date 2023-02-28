using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace test_task_2002.Models
{
    public class ApplicationContext: DbContext
    {

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; } 
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
           // Database.EnsureDeleted();
            Database.EnsureCreated();   

        }


    }
}
