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

        // Adaug� o proprietate pentru leg�tura cu Job
        public Job Job { get; set; }
    }
}
