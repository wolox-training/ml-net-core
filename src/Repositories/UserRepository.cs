using Microsoft.EntityFrameworkCore;
using MlNetCore.Models;
using MlNetCore.Repositories.Database;
using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
