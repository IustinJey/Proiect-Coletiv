// User_Badge.cs
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class UserBadge
    {
        [Key]
        public int UserBadgeId { get; set; }
        public int IdUser { get; set; }
        public int IdBadge { get; set; }

        // Adaugã o legãturã la tabela User
        public User User { get; set; }

        // Adaugã o legãturã la tabela Badge
        public Badge Badge { get; set; }
    }
}
