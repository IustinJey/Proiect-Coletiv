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
        // Navigation property for related images
        public List<JobImage> Images { get; set; }

        // Alte proprietăți specifice job-ului

        // Adaugă o proprietate pentru legătura cu User
        public int IdUser { get; set; }
        public User User { get; set; }
        // Adaugă o colecție de recenzii pentru job
        public List<ReviewJob> ReviewsJob { get; set; }
    }
}
