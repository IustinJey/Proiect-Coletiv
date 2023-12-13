// Review.cs
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class ReviewJob
    {
        [Key]
        public int IdReviewJ { get; set; }
        public int IdJob { get; set; }
        public int Rating { get; set; } 
        public Job Job { get; set; }
    }
}
