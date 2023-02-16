using DBGameDatabaseContextDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;
using System.Text.Json;

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
        public IEnumerable<Game> Get()
        {
            return _db.Games;
        }

        //Add game
        [HttpPost("addGame")]
        public bool addGamePOST([FromBody] JsonElement userinput)
        {
            Game newGame = new Game();
            newGame.Name = userinput.GetProperty("name").ToString();
            newGame.Description = userinput.GetProperty("description").ToString();
            newGame.Genre = userinput.GetProperty("genre").ToString();
            newGame.Platform = userinput.GetProperty("platform").ToString();
            newGame.ReleaseDate = DateTime.Parse(userinput.GetProperty("releasedate").ToString());
            newGame.ExpectedRuntime = Int32.Parse(userinput.GetProperty("runtime").ToString());


            string base64string = userinput.GetProperty("imageUrl").ToString();
            System.Diagnostics.Debug.WriteLine(base64string);
            System.Diagnostics.Debug.WriteLine(userinput.GetProperty("imageUrl").ToString());

            Image newImage = new Image();
            byte[] bytes = Convert.FromBase64String(userinput.GetProperty("imageUrl").ToString());
            newImage.CoverImage = bytes;
            newGame.CoverImage = bytes;
            System.Diagnostics.Debug.WriteLine("output:");
            System.Diagnostics.Debug.WriteLine(bytes);

            _db.Games.Add(newGame);
            _db.Images.Add(newImage);
            _db.SaveChanges();
            return true;
        }











        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
