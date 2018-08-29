using System.Linq;
using Microsoft.EntityFrameworkCore;
using MlNetCore.Models;
using MlNetCore.Repositories.Database;
using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context)
        {
        }

        public DataBaseContext MovieContext
        {
            get { return Context as DataBaseContext; }
        }
    }
}
