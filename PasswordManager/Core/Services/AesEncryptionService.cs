using System.Security.Cryptography;
using System.Text;
using PasswordManager.Core.Interfaces;

namespace PasswordManager.Core.Services;

public class AesEncryptionService : IEncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public AesEncryptionService(string password)
    {
        using var keyGen = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("MySalt123"), 10000);
        _key = keyGen.GetBytes(32);
        _iv = keyGen.GetBytes(16);
    }

    public byte[] Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var encryptor = aes.CreateEncryptor();
        var bytes = Encoding.UTF8.GetBytes(plainText);

        return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
    }

    public string Decrypt(byte[] cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var decryptor = aes.CreateDecryptor();
        var decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

        return Encoding.UTF8.GetString(decrypted);
    }
}