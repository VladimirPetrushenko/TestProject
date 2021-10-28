using MyModelAndDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
