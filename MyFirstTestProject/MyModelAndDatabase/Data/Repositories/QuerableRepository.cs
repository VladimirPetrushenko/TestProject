using Microsoft.EntityFrameworkCore;
using MyModelAndDatabase.Data.Context;
using MyModelAndDatabase.Data.Interfaces;
using MyModelAndDatabase.Models.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data.Repositories
{
    public class QuerableRepository<TEntity> : IQuerableRepository<TEntity>
        where TEntity: class, IId
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly MyContext _context;

        public QuerableRepository(MyContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void CreateItem(TEntity item) =>
            _dbSet.Add(item);

        public void DeleteItem(TEntity item) =>
            _dbSet.Remove(item);

        public IQueryable<TEntity> GetAll() =>
            _dbSet.AsNoTracking();

        public Task<TEntity> GetByID(int id) =>
            Task.FromResult(_dbSet.Find(id));

        public void UpdateItem(TEntity item) =>
            _context.Entry(item).State = EntityState.Modified;

        public async Task<bool> SaveChanges() =>
            await _context.SaveChangesAsync() >= 0;

        public Task<bool> ItemExists(int id) =>
            _dbSet.AsNoTracking().AnyAsync(x => x.Id == id);
    }
}
