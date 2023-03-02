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
        public int Id { get; set; }
        [Required]
        public byte[] Bytes { get; set; }
        public List<Game> UsedWithGames { get; set; }
    }
}
