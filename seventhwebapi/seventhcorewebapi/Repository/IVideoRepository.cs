using SeventhCoreWebAPI.Models;

namespace SeventhCoreWebAPI.Repositories
{
    public interface IVideoRepository : IGenericRepository<Video>
    {
        Task<IEnumerable<Video>> GetServerVideos(string serverId);
    }
}