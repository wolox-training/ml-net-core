using Microsoft.EntityFrameworkCore;
using MlNetCore.Models;
using MlNetCore.Repositories.Database;
using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
