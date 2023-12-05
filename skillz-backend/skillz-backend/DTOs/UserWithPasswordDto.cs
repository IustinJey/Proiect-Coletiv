using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using skillz_backend.models;

namespace skillz_backend.DTOs
{
    public class UserWithPasswordDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Location { get; set; }

        public bool Verified { get; set; }

        public string ProfilePicture { get; set; }

        public List<Job> Jobs { get; set; }

        public List<ReviewUser> ReviewsUser { get; set; }

        public List<CertificatUser> UserCertificates { get; set; }

        public List<UserBadge> UserBadges { get; set; }
    }
}