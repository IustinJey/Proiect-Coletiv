using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents a type of certificate.
    public class Certificate
    {
        // Gets or sets the unique identifier for the certificate.
        [Key]
        public int IdCertificate { get; set; }

        // Gets or sets the type of certificate.
        public string CertificateType { get; set; }

        // Gets or sets the list of user certificates associated with this certificate type.
        public List<CertificatUser> UserCertificates { get; set; }
    }
}
