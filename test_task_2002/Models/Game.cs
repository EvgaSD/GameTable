using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace test_task_2002.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? GameName { get; set; }

        public int CountOfPoints { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; } 
    }
}
