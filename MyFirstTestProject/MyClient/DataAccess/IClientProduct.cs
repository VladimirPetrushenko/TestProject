using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyClient.DataAccess
{
    public interface IClientProduct
    {
        [Get("/Product/{id}")]
        Task<Product> GetProduct(int id);

        [Get("/Product/GetAll")]
        Task<List<Product>> GetProducts();

        [Post("/Product/")]
        Task<Product> CreateProduct([Body] AddProduct product);

        [Put("/Product/")]
        Task<Product> UpdateProduct([Body] UpdateProduct product);

        [Delete("/Product/")]
        Task<Product> DeleteProducts([Body] DeleteProduct  product);
    }
}
