#region Using
using Microsoft.EntityFrameworkCore;
using MlNetCore.Models;
#endregion

namespace MlNetCore.Repositories.Database
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<DataBaseContext> options) : base(options){}

        public virtual DbSet<Movie> Movies { get; set; }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.Add(modelBuilder);
        }
    }
}
