using Asymmetrisk_kryptering;
using System.Text;

RSA rsa = new RSA();

WriteToXML();
Console.ReadLine();


void WriteToXML()
{
    string textToEncrypt = "Hello World";

    //path for keys to be stored
    string publicKeyPath = @"C:\RSAKeys\publicKey.xml";
    string privateKeyPath = @"C:\RSAKeys\privateKey.xml";

    //Assign new keys to the path   
    rsa.AssignNewKey(publicKeyPath, privateKeyPath);

    //Encrypt and decrypt or text
    byte[] encrypted = rsa.Encrypt(publicKeyPath, Encoding.UTF8.GetBytes(textToEncrypt));
    byte[] decrypted = rsa.Decrypt(privateKeyPath, encrypted);

    //Write or data to the console.
    Console.WriteLine($"Key stored in XML");
    Console.WriteLine($"Plain text: {textToEncrypt}");
    Console.WriteLine($"Encrypted: {Convert.ToBase64String(encrypted)}");
    Console.WriteLine($"Decrypted: {Encoding.Default.GetString(decrypted)}");
}