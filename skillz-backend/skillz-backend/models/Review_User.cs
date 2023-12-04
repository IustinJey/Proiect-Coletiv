// Review.cs
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class ReviewUser
    {
        [Key]
        public int IdReviewU { get; set; }
        public int IdUser { get; set; }
        public int Rating { get; set; }
        public User User { get; set; }
    }
}
