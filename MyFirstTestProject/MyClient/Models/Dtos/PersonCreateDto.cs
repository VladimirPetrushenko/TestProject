﻿using System.ComponentModel.DataAnnotations;

namespace MyClient.Models.Persons
{
    public class PersonCreateDto
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
    }
}