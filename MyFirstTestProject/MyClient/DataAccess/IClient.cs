using MyClient.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyClient.DataAccess
{
    public interface IClient
    {
        [Get("/Product/{id}")]
        Task<Product> GetProduct(int id);

        [Get("/Product/GetAll")]
        Task<List<Product>> GetProducts();

        [Post("/Product/")]
        Task CreateProduct([Body] Product product);
    }
}
