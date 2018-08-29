using Microsoft.EntityFrameworkCore;
using MlNetCore.Models;
using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
