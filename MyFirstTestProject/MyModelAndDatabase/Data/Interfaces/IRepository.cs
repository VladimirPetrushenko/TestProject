using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<T> GetByID(int id);
        void CreateItem(T item);
        void UpdateItem(T item);
        void DeleteItem(T item);
        Task<bool> SaveChanges();
        Task<bool> ItemExists(int id);
    }
}
