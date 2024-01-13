// Job.cs
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class Job
    {
        [Key]
        public int IdJob { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int ExperiencedYears { get; set; }

        public List<JobImage> Images { get; set; }  
        public int IdUser { get; set; }
        public User User { get; set; }
        public List<ReviewJob> ReviewsJob { get; set; }
    }
}
