using MyModelAndDatabase.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyModelAndDatabase.Models
{
    public class Person : ICloneable, IId
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public bool IsBlock { get; set; }

        public object Clone() =>
            new Person { Id = this.Id, FirstName = this.FirstName, LastName = this.LastName };
    }
}
