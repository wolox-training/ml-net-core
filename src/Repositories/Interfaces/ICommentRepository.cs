using System.Collections.Generic;
using MlNetCore.Models;

namespace MlNetCore.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        List<Comment> GetAllCommentsOfMovie(int id);
    }
}
