namespace MyModelAndDatabase.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
    }
}
