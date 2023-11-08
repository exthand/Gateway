using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BankSettings
    {
        public string ncaId { get; set; }
        public string appClientId { get; set; }
        public string appClientSecret { get; set; }
        public string tlsCertificateName { get; set; }
        public string tlsCertificatePassword { get; set; }
        public string signingCertificateName { get; set; }
        public string signingCertificatePassword { get; set; }
        public string appApiKey { get; set; }
        public string pemFileUrl { get; set; }
        public string signingCertificateKeyId { get; set; }
        public string tlsCertificateKeyId { get; set; }
        public string encryptionCertificateName { get; set; }
        public string encryptionCertificatePassword { get; set; }
        public string encryptionCertificateKeyId { get; set; }

        public string bankTlsPublicKeyName { get; set; }
        public string bankSigningPublicKeyName { get; set; }
        public string bankEncryptionPublicKeyName { get; set; }
    }
}
