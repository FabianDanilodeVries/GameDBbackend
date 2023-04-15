using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DBGameDatabaseContextDB
{
    public class Game
    {
        public int Id { get; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public int ExpectedRuntime { get; set; }
        [Required]
        public Image CoverImage { get; set; }
    }
}
