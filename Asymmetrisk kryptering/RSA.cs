using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;

namespace Asymmetrisk_kryptering
{
    internal class RSA
    {

        //Generate public and private key. To asigned paths
        public void AssignNewKey(string publicKeyPath, string privateKeyPath)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;

                if (File.Exists(privateKeyPath))
                {
                    File.Delete(privateKeyPath);
                }

                if (File.Exists(publicKeyPath))
                {
                    File.Delete(publicKeyPath);
                }

                var publicKeyfolder = Path.GetDirectoryName(publicKeyPath);
                var privateKeyfolder = Path.GetDirectoryName(privateKeyPath);

                if (!Directory.Exists(publicKeyfolder))
                {
                    Directory.CreateDirectory(publicKeyfolder);
                }

                if (!Directory.Exists(privateKeyfolder))
                {
                    Directory.CreateDirectory(privateKeyfolder);
                }

                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));
            }
        }

        //Encrypt or message with the public key
        public byte[] Encrypt(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] cipherbytes;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));

                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }

            return cipherbytes;
        }

        //Decrypt the encrypted message with the private key
        public byte[] Decrypt(string privateKeyPath, byte[] dataToEncrypt)
        {
            byte[] plain;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
                plain = rsa.Decrypt(dataToEncrypt, false);
            }

            return plain;
        }
    }
}
