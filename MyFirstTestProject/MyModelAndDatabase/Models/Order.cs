using MyModelAndDatabase.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyModelAndDatabase.Models
{
    public class Order : IId
    {
        [Key]
        public int Id { get; set; }
        public Person Person { get; set; }
        public List<Product> Products { get; set; }
    }
}
