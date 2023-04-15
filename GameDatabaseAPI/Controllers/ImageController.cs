using DBGameDatabaseContextDB;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private GameDatabaseContext _db;

        public ImageController(GameDatabaseContext db)
        {
            _db = db;
        }


    }
}
