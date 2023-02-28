using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using test_task_2002.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace test_task_2002.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private ApplicationContext db;
        //ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
             db = context;
            if (db.Users.Count() == 0)
            {
                User user1 = new User { Name = "Alex Potter", Age = 26 ,Status="newGamer",NickName="Alex"};
                User user2 = new User { Name = "Luna Lavgood", Age = 24, Status = "newGamer", NickName = "Luna" };
                User user3 = new User { Name = "Tom Riddle", Age = 25, Status = "expiriencedGamer", NickName = "Tom" };

                Game game1 = new Game { GameName = "LocalGame", CountOfPoints = 10,User=user1 };
                Game game2= new Game { GameName = "NetflixGame",CountOfPoints=33, User = user1 };
                Game game3 = new Game { GameName = "Game_08022003",CountOfPoints=66 , User = user2 };
                Game game4 = new Game { GameName = "AppleGame", CountOfPoints = 77, User = user3 };

                db.Users.AddRange(user1,user2,user3);
                db.Games.AddRange(game1,game2,game3,game4);
                db.SaveChanges();
            }


        }
       
        public ActionResult IndexWithFilter(int? user, string? name)
        {
            IQueryable<Game> games = db.Games.Include(p => p.User);
            if (user != null && user != 0)
            {
                games = games.Where(p => p.UserId == user);
            }
            if (!string.IsNullOrEmpty(name))
            {
               games = games.Where(p => p.GameName!.Contains(name));
            }

            List<User> users = db.Users.ToList();
            users.Insert(0, new User { Name = "All", Id = 0 });

            GamesListViewModel viewModel = new GamesListViewModel
            {
                Games= games.ToList(),
                Users = new SelectList(users, "Id", "Name",user),
                Name = name
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Index()
        {
            var users = db.Users.Include(c => c.Games).ToList();
            return View(await db.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            SelectList users = new SelectList(db.Users, "Id", "Name");
            ViewBag.Users = users;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            db.Games.Add(game);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexWithFilter");
        }

        public async Task<IActionResult> AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexWithFilter");
        }
        public IActionResult DetailsUsers(int? id)
        {
            if (id != null)
            {
               User user = db.Users.FirstOrDefault(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        
        //--------------------------------------------------------------------------------------------------------
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Game game = await db.Games.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
                if (game != null)
                {
                    SelectList users = new SelectList(db.Users, "Id", "Name", game.UserId);
                    ViewBag.Users = users;
                    return View(game);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Game game)
        {
            db.Games.Update(game);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexWithFilter");
        }
        //---------------------------------------------------------------------------------
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Game game = await db.Games.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
                if (game!= null)
                    return View(game);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Game game = await db.Games.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
                if (game != null)
                {
                    db.Games.Remove(game);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexWithFilter");
                }
            }
            return NotFound();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
