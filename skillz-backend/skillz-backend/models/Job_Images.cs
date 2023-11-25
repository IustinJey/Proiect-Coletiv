// JobImage.cs
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class JobImage
    {
        [Key]
        public int IdImage { get; set; }
        public string ImageUrl { get; set; }

        // Foreign key to Job
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
