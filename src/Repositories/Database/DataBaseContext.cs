#region Using
using Microsoft.EntityFrameworkCore;
using MlNetCore.Models;
#endregion

namespace MlNetCore.Repositories.Database
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) {}

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
