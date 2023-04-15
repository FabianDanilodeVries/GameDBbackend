using DBGameDatabaseContextDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameDatabaseAPI.Controllers
{
    public class UserController : Controller
    {
        private GameDatabaseContext _db;

        public UserController(GameDatabaseContext db)
        {
            _db = db;
        }
    }
}
