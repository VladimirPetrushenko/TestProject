using MyModelAndDatabase.Models.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data.Interfaces
{
    public interface IQuerableRepository<TEntity>
        where TEntity : class, IId
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByID(int id);
        void CreateItem(TEntity item);
        void UpdateItem(TEntity item);
        void DeleteItem(TEntity item);
        Task<bool> SaveChanges();
        Task<bool> ItemExists(int id);
    }
}
