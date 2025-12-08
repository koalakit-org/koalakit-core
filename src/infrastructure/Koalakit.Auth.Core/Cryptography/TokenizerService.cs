using System.Security.Cryptography;
using System.Text;

namespace Koalakit.Auth.Core.Cryptography;

/// <summary>
/// Service for encrypting and decrypting sensitive data using AES-256 encryption.
/// </summary>
public class TokenizerService : ITokenizerService
{
    private readonly byte[] _encryptionKey;

    /// <summary>
    /// Initializes a new instance of the TokenizerService with a byte array encryption key.
    /// </summary>
    /// <param name="encryptionKey">The encryption key (must be exactly 32 bytes for AES-256)</param>
    /// <exception cref="ArgumentException">Thrown when encryption key is not exactly 32 bytes</exception>
    public TokenizerService(byte[] encryptionKey)
    {
        if (encryptionKey == null || encryptionKey.Length != 32)
        {
            throw new ArgumentException("Encryption key must be exactly 32 bytes (256 bits)", nameof(encryptionKey));
        }
        
        _encryptionKey = encryptionKey;
    }

    /// <summary>
    /// Initializes a new instance of the TokenizerService with a string encryption key.
    /// </summary>
    /// <param name="encryptionKey">The encryption key (will be UTF-8 encoded, must be 32 bytes)</param>
    /// <exception cref="ArgumentException">Thrown when encryption key is not exactly 32 bytes</exception>
    public TokenizerService(string encryptionKey)
        : this(Encoding.UTF8.GetBytes(encryptionKey ?? throw new ArgumentNullException(nameof(encryptionKey))))
    {
    }

    /// <inheritdoc/>
    public string Tokenize(string? plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
        {
            return string.Empty;
        }

        using var aesAlg = Aes.Create();
        aesAlg.Key = _encryptionKey;
        aesAlg.GenerateIV();
        var iv = aesAlg.IV;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Padding = PaddingMode.PKCS7;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        var encryptedContent = msEncrypt.ToArray();
        var result = new byte[iv.Length + encryptedContent.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(encryptedContent, 0, result, iv.Length, encryptedContent.Length);
        return Convert.ToBase64String(result);
    }

    /// <inheritdoc/>
    public string Detokenize(string protectedText)
    {
        if (string.IsNullOrWhiteSpace(protectedText))
        {
            return string.Empty;
        }

        var fullCipher = Convert.FromBase64String(protectedText);
        using var aesAlg = Aes.Create();
        aesAlg.Key = _encryptionKey;
        var iv = new byte[aesAlg.BlockSize / 8];
        var cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);
        aesAlg.IV = iv;
        aesAlg.Mode = CipherMode.CBC;
        aesAlg.Padding = PaddingMode.PKCS7;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}

