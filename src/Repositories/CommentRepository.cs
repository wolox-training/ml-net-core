using System.Collections.Generic;
using System.Linq;
using MlNetCore.Models;
using Microsoft.EntityFrameworkCore;
using MlNetCore.Repositories.Database;
using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataBaseContext context) : base(context)
        {
        }

        public DataBaseContext CommentContext
        {
            get { return Context as DataBaseContext; }
        }

        public List<Comment> GetAllCommentsOfMovie(int id)
        {
            return CommentContext.Comment.Where(m => m.Movie.Id == id).ToList();
        }
    }
}
