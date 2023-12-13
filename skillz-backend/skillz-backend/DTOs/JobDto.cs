using skillz_backend.models;

namespace skillz_backend.DTOs
{
    public class JobDto
    {
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int ExperiencedYears { get; set; }
        public int IdUser { get; set; }
    }
}
