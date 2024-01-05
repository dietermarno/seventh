namespace SeventhCoreWebAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task Insert(T obj);
        Task Update(string id, T obj);
        Task Delete(string id);
    }
}