using System.Threading.Tasks;
using MlNetCore.Models;
using MlNetCore.Models.VO;

namespace MlNetCore.Repositories.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<MovieVO> GetFilteredWithOrder(string movieGenre, string searchString, string sortOrder, int? pageIndex);
        Movie GetMovieWithComments(int id);
        void RemoveMovieCascade(Movie movie);
    }
}
