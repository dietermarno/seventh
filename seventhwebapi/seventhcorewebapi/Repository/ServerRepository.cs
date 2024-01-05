
using Microsoft.EntityFrameworkCore;
using SeventhCoreWebAPI.Models;

namespace SeventhCoreWebAPI.Repositories
{
    public class ServerRepository : GenericRepository<Server>, IServerRepository
    {
        private AppDbContext context = null;

        public ServerRepository(AppDbContext repositoryContext)
             : base(repositoryContext)
        {
            context = repositoryContext;
        }
        public async Task<IEnumerable<Server>> ServerExists(string serverId)
        {
            return await context.Servers
                .Where(server => server.Id == serverId)
                .AsNoTracking().ToListAsync();
        }
    }
}