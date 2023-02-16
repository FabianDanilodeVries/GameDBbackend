using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBGameDatabaseContextDB
{
    public class GameDatabaseContext : DbContext
    {
        public GameDatabaseContext(DbContextOptions options) : base(options) {}
        public DbSet<Game> Games { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
