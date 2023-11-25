// Badge.cs
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class Badge
    {
        [Key]
        public int BadgeId { get; set; }
        public string BadgeType { get; set; }
        public string Icon { get; set; } // Icon reprezintă imaginea în format de byte[]

        // Adaugă o colecție de legături către utilizatori
        public List<UserBadge> UserBadges { get; set; }
    }
}
