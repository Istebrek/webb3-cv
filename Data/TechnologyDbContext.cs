using Microsoft.EntityFrameworkCore;
using SQL_API_Istebrek_Web.Models;

namespace SQL_API_Istebrek_Web.Data
{
    public class TechnologyDbContext : DbContext
    {
        public TechnologyDbContext(DbContextOptions<TechnologyDbContext> options) : base(options) { }

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
