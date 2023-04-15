using DBGameDatabaseContextDB;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private GameDatabaseContext _db;

        public NewsController(GameDatabaseContext db)
        {
            _db = db;
        }

        //Load 6 latest news articles
        [HttpGet("loadLatestNews")]
        public IEnumerable<LatestNewsDto> GetLatestNews()
        {
            //Create TopGameDto object and add data + cover image as Base64String
            var news = _db.NewsItems.Select(n => new LatestNewsDto()
            {
                Id = n.Id,
                Title = n.Title,
                Excerpt = n.Excerpt,
                Body = n.Body,
                PostDate = n.PostDate.ToString("HH:mm MMMM dd yyyy"),
                Base64String = Convert.ToBase64String(n.CoverImage.Bytes, 0, n.CoverImage.Bytes.Length, Base64FormattingOptions.None)
            })
            .Take(6) //Select top 6 results
            .ToList() //Turn results into a list
            .OrderByDescending(n => DateTime.Parse(n.PostDate)); //Order by date
            return news;
        }

        //Add News
        [HttpPost("addNews")]
        public bool AddNewsPOST([FromBody] JsonElement userinput)
        {
            //Create new News instance
            News newNews = new News();
            newNews.Title = userinput.GetProperty("title").ToString();
            newNews.Excerpt = userinput.GetProperty("excerpt").ToString();
            newNews.Body = userinput.GetProperty("body").ToString();
            newNews.PostDate = DateTime.Now;

            //Check if header code if present on image base64string. If so, remove it and convert to base64String.
            byte[] bytes;
            if (userinput.GetProperty("imageUrl").ToString().Contains(','))
            {
                string imageUrltoString = userinput.GetProperty("imageUrl").ToString();
                string validBase64String = imageUrltoString.Substring(imageUrltoString.LastIndexOf(',') + 1);
                bytes = Convert.FromBase64String(validBase64String);
            }
            else
            {
                bytes = Convert.FromBase64String(userinput.GetProperty("imageUrl").ToString());
            }

            //Check if image already exists. If so, add existing image instead of creating a new one.
            var result = from i in _db.Images
                         where i.Bytes == bytes
                         select i;
            if (result.Count() != 0)
            {
                newNews.CoverImage = result.FirstOrDefault();
            }
            else
            {
                Image newImage = new Image();
                newImage.Bytes = bytes;
                newNews.CoverImage = newImage;
                _db.Images.Add(newImage);
            }

            //Save changes to the database
            _db.NewsItems.Add(newNews);
            _db.SaveChanges();
            return true;
        }
    }
}
