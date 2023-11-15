using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace skillz_backend.models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Location { get; set; }

        public bool Verified { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public byte[] Salt { get; set; }
    }
}