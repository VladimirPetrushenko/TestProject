using AutoFixture.Xunit2;
using MyClient.DataAccess;
using MyClient.Models.Products;
using MyModelAndDatabase.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestForMyProject.IntegrationTests
{
    public class ProductTests
    {
        private readonly IClientProduct _client;
        public ProductTests()
        {
            _client = RestService.For<IClientProduct>("https://localhost:44334");
        }

        [Theory, AutoData]
        public async Task AddProductTestAsync(AddProduct product)
        {
            var countProduct = (await _client.GetProducts()).Count;
            var response = await _client.CreateProduct(product);

            if(countProduct + 1 != (await _client.GetProducts()).Count)
            {
                throw new Exception();
            }

            var delete = await _client.DeleteProducts(new DeleteProduct { Id = response.Id });

            if (countProduct != (await _client.GetProducts()).Count)
            {
                throw new Exception();
            }
        }
    }
}
