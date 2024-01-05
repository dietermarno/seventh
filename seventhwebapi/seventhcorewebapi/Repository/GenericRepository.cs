
using Microsoft.EntityFrameworkCore;
using SeventhCoreWebAPI.Models;

namespace SeventhCoreWebAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext context = null;
        public GenericRepository(AppDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetById(string id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task Insert(T obj)
        {
            await context.Set<T>().AddAsync(obj);
            await context.SaveChangesAsync();
        }
        public async Task Update(string id, T obj)
        {
            context.Set<T>().Update(obj);
            await context.SaveChangesAsync();
        }
        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}