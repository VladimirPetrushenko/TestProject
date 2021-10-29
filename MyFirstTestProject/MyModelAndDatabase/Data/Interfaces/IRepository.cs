using System.Collections.Generic;

namespace MyModelAndDatabase.Data.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        void CreateItem(T item);
        void UpdateItem(T item);
        void DeleteItem(T item);
        bool SaveChanges();
        bool ItemExists(int id);
    }
}
