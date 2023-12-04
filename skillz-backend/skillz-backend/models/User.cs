// User.cs
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Collections.Generic;

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
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public string ProfilePicture { get; set; }

        public List<Job> Jobs { get; set; }

        public List<ReviewUser> ReviewsUser { get; set; }

        public List<CertificatUser> UserCertificates { get; set; }

        public List<UserBadge> UserBadges { get; set; }
    }
}
