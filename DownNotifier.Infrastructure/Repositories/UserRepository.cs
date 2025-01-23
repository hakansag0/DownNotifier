using DownNotifier.Application.Repositories;
using DownNotifier.Domain.Entities;
using DownNotifier.Infrastructure.Context;
using DownNotifier.Infrastructure.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Infrastructure.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DownNotifierDbContext downNotifierDbContext) : base(downNotifierDbContext) { }

        public User? GetByEmail(string email)
        {
            return downNotifierDbContext.Set<User>().FirstOrDefault(s => s.Email == email);
        }
    }
}
