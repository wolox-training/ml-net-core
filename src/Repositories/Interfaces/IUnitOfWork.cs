using System;

namespace MlNetCore.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        int Complete();       
    }
}
