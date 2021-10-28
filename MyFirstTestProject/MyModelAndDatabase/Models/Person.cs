using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyModelAndDatabase.Models
{
    public class Person : ICloneable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public object Clone()
        {
            return new Person { Id = this.Id, FirstName = this.FirstName, LastName = this.LastName };
        }
    }
}
