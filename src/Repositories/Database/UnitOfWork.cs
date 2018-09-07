using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Repositories.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
            MovieRepository = new MovieRepository(_context);
            UserRepository = new UserRepository(_context);
            CommentRepository = new CommentRepository(_context);
        }

        public IMovieRepository MovieRepository { get; private set; }

        public ICommentRepository CommentRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
