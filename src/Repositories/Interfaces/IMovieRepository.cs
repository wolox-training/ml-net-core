using System.Threading.Tasks;
using MlNetCore.Models;
using MlNetCore.Models.VO;

namespace MlNetCore.Repositories.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<MovieVO> GetFiltered(string movieGenre, string searchString);
    }
}
