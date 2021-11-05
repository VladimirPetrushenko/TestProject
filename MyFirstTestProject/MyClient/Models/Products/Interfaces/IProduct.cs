using MyModelAndDatabase.Models;

namespace MyClient.Models.Products.Interfaces
{
    public interface IProduct
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public decimal Price { get; set; }
    }
}
