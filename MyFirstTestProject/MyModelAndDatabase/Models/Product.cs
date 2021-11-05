using MyModelAndDatabase.Models.Interfaces;

namespace MyModelAndDatabase.Models
{
    public class Product : IId
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public decimal Price { get; set; } 
    }
}
