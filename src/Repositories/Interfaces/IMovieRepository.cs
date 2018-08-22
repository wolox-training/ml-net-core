using MlNetCore.Models;

namespace MlNetCore.Repositories.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Movie GetMovie(int id);
    }
}
