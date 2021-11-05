using MyClient.Models.Dtos.Products;
using MyClient.Models.Persons;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.Models.Dtos.Orders
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public PersonReadDto Person { get; set; }
        public List<ProductReadDto> Products { get; set; }
        public decimal OrderPrice { get => Products.Sum(x => x.Price); }
    }
}
