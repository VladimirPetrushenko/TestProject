using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyClient.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
    }
}
