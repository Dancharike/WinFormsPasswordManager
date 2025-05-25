using System.Security.Cryptography;
using System.Text;
using PasswordManager.Core.Interfaces;

namespace PasswordManager.Core.Services;

/// <summary>
/// Service for encrypting and decrypting text using AES with a password-based key.
/// Implements IEncryptionService.
/// </summary>
public class AesEncryptionService : IEncryptionService
{
    private readonly byte[] _key; // 256-bit encryption key
    private readonly byte[] _iv;  // 128-bit initialization vector

    /// <summary>
    /// Initializes the AES encryption service using a password.
    /// The key and IV are derived from the password using PBKDF2.
    /// </summary>
    public AesEncryptionService(string password)
    {
        // derive key and IV from password using fixed salt and iteration count
        using var keyGen = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("MySalt123"), 10000);
        _key = keyGen.GetBytes(32); // AES-256 key
        _iv = keyGen.GetBytes(16);  // AES block size = 128 bits
    }

    /// <summary>
    /// Encrypts plain text using AES and returns the encrypted byte array.
    /// </summary>
    public byte[] Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var encryptor = aes.CreateEncryptor();
        var bytes = Encoding.UTF8.GetBytes(plainText);

        // encrypt the plain text bytes
        return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// Decrypts an AES-encrypted byte array and returns the original plain text.
    /// </summary>
    public string Decrypt(byte[] cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var decryptor = aes.CreateDecryptor();
        var decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

        // decode decrypted bytes back to string
        return Encoding.UTF8.GetString(decrypted);
    }
}