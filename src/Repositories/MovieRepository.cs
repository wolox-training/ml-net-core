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

        public MovieContext MovieContext
        {
            get { return Context as MovieContext; }
        }

        public Movie GetMovie(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
