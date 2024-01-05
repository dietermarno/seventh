
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    }
}