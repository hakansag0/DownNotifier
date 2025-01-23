using DownNotifier.Application.Repositories.Shared;
using DownNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public User? GetByEmail(string email);
    }
}
