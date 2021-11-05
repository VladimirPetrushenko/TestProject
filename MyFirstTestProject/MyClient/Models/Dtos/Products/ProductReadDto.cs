using MyModelAndDatabase.Models;

namespace MyClient.Models.Dtos.Products
{
    public class ProductReadDto
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public decimal Price { get; set; }
    }
}
