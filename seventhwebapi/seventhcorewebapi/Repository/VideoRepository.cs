
using Microsoft.EntityFrameworkCore;
using SeventhCoreWebAPI.Models;

namespace SeventhCoreWebAPI.Repositories
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        private AppDbContext context = null;

        public VideoRepository(AppDbContext repositoryContext)
             : base(repositoryContext)
        {
            context = repositoryContext;
        }

        public async Task<IEnumerable<Video>> GetServerVideos(string serverId)
        {
            return await context.Videos.Where(video => video.ServerId == serverId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> IsServerAvailable(string serverId)
        {
            var serverList = await context.Servers.Where(server => server.Id == serverId).AsNoTracking().ToListAsync();
            return serverList.Any();
        }
    }
}