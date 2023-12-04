using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace skillz_backend.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Location { get; set; }
    }
}