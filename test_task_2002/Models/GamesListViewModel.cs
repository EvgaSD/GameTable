using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace test_task_2002.Models
{
    public class GamesListViewModel
    {
        public IEnumerable<Game> Games { get; set; } = new List<Game>();
        public SelectList Users { get; set; } = new SelectList(new List<User>(), "Id", "Name");
        public string? Name { get; set; }
    }
}
