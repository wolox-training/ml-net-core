using System;

namespace MlNetCore.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        ICommentRepository CommentRepository { get; }
        IUserRepository UserRepository { get; }
        int Complete();       
    }
}
