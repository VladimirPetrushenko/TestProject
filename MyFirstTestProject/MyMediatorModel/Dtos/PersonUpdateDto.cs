using System.ComponentModel.DataAnnotations;

namespace MyApi.Dtos
{
    public class PersonUpdateDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
    }
}
