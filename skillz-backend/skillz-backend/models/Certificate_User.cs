// Certificat_User.cs
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class CertificatUser
    {
        [Key]
        public int IdCertificatUser { get; set; }
        public int IdCertificate { get; set; }
        public int IdUser { get; set; }
        public string CertificateImage { get; set; }

        // Adaugã o legãturã la tabela Certificate
        public Certificate Certificate { get; set; }

        // Adaugã o legãturã la tabela User
        public User User { get; set; }
    }
}
