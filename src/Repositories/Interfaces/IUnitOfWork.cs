using System;

namespace MlNetCore.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository Movies { get; }
        int Complete();
    }
}
