using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Data.Interfaces
{
    public interface IQuerableRepository<T>
    {
        IQueryable<T> GetItemsWithName(string name);
        IQueryable<T> GetAll();
        Task<T> GetByID(int id);
        void CreateItem(T item);
        void UpdateItem(T item);
        void DeleteItem(T item);
        Task<bool> SaveChanges();
        Task<bool> ItemExists(int id);
    }
}
