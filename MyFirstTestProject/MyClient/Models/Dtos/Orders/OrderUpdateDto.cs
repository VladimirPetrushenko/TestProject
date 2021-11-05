using System.Collections.Generic;

namespace MyClient.Models.Dtos.Orders
{
    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public List<int> Products { get; set; }
    }
}
