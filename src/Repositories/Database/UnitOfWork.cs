using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Repositories.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _context;

        public UnitOfWork(MovieContext context)
        {
            _context = context;
            Movies = new MovieRepository(_context);
        }

        public IMovieRepository Movies { get; private set; }

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
