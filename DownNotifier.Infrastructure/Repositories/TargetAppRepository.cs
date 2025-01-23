using DownNotifier.Application.Repositories;
using DownNotifier.Domain.Entities;
using DownNotifier.Infrastructure.Context;
using DownNotifier.Infrastructure.Repositories.Shared;

namespace DownNotifier.Infrastructure.Repositories
{
    internal class TargetAppRepository : GenericRepository<TargetApp>, ITargetAppRepository
    {
        public TargetAppRepository(DownNotifierDbContext downNotifierDbContext) : base(downNotifierDbContext) { }
    }
}
