using SeventhCoreWebAPI.Models;

namespace SeventhCoreWebAPI.Repositories
{
    public interface IServerRepository : IGenericRepository<Server>
    {
        Task<IEnumerable<Server>> ServerExists(string server);
    }
}