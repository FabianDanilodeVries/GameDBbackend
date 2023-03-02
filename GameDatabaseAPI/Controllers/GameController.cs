using DBGameDatabaseContextDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using Image = DBGameDatabaseContextDB.Image;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameDatabaseContext _db;

        public GameController(GameDatabaseContext db)
        {
            _db = db;
        }

        //Load top games
        [HttpGet("loadTopGames")]
        public IEnumerable<TopGameDto> GetTopGames()
        {
            //Create TopGameDto object and add data + cover image as Base64String
            var games = from g in _db.Games
                        select new TopGameDto()
                        {
                            Id = g.Id,
                            Name = g.Name,
                            Description = g.Description,
                            Genre = g.Genre,
                            Platform = g.Platform,
                            ReleaseDate = g.ReleaseDate,
                            ExpectedRuntime = g.ExpectedRuntime,
                            Base64String = Convert.ToBase64String(g.CoverImage.Bytes, 0, g.CoverImage.Bytes.Length, Base64FormattingOptions.None)
                        };
            return games;
        }

        //Add game
        [HttpPost("addGame")]
        public bool AddGamePOST([FromBody] JsonElement userinput)
        {
            //Create new game instance
            Game newGame = new Game();
            newGame.Name = userinput.GetProperty("name").ToString();
            newGame.Description = userinput.GetProperty("description").ToString();
            newGame.Genre = userinput.GetProperty("genre").ToString();
            newGame.Platform = userinput.GetProperty("platform").ToString();
            newGame.ReleaseDate = DateTime.Parse(userinput.GetProperty("releasedate").ToString());
            newGame.ExpectedRuntime = Int32.Parse(userinput.GetProperty("runtime").ToString());

            //Check if header code if present on image base64string. If so, remove it and convert to base64String.
            byte[] bytes;
            if (userinput.GetProperty("imageUrl").ToString().Contains(','))
            {
                string imageUrltoString = userinput.GetProperty("imageUrl").ToString();
                string validBase64String = imageUrltoString.Substring(imageUrltoString.LastIndexOf(',') + 1);
                bytes = Convert.FromBase64String(validBase64String);
            } else
            {
                bytes = Convert.FromBase64String(userinput.GetProperty("imageUrl").ToString());
            }

            //Check if image already exists. If so, add existing image instead of creating a new one.
            var result = from i in _db.Images
                         where i.Bytes == bytes
                         select i;
            if (result.Count() != 0)
            {
                newGame.CoverImage = result.FirstOrDefault();
            } else
            {
                Image newImage = new Image();
                newImage.Bytes = bytes;
                newGame.CoverImage = newImage;
                _db.Images.Add(newImage);
            }

            //Save changes to the database
            _db.Games.Add(newGame);
            _db.SaveChanges();
            return true;
        }

    }
}
