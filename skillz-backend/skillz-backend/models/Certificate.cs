// Certificate.cs
using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    public class Certificate
    {
        [Key]
        public int IdCertificate { get; set; }
        public string CertificateType { get; set; }

        // Adaugă o colecție de legături către utilizatori
        public List<CertificatUser> UserCertificates { get; set; }
    }
}
