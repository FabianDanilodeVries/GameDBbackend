using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBGameDatabaseContextDB
{
    public class Image
    {
        public int Id { get; }
        [Required]
        public byte[] Bytes { get; set; }
        public List<Game> UsedWithGames { get; set; }
        public List<News> UsedWithNews { get; set; }
    }
}
