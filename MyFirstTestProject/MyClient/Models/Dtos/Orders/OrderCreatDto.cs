using System.Collections.Generic;

namespace MyClient.Models.Dtos.Orders
{
    public class OrderCreatDto
    {
        public int Person { get; set; }
        public List<int> Products { get; set; }
    }
}
