using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBGameDatabaseContextDB
{
    public class News
    {
        public int Id { get; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Excerpt { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime PostDate { get; set; }
        [Required]
        public Image CoverImage { get; set; }
    }
}
