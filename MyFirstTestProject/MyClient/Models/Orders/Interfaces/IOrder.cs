using System.Collections.Generic;

namespace MyClient.Models.Orders.Interfaces
{
    public interface IOrder
    {
        int Person { get; set; }
        List<int> Products { get; set; }
    }
}
