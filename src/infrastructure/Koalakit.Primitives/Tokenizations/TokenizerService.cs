using System.Security.Cryptography;
using System.Text;

namespace Kamel.Primitives.Tokenizations;

public static class TokenizerService
{
    private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("12345678901234567890123456789012");

    public static string Tokenize(this string? plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
        {
            return string.Empty;
        }

        using var aesAlg = Aes.Create();
        aesAlg.Key = EncryptionKey;
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

    public static string Detokenize(this string protectedText)
    {
        if (string.IsNullOrWhiteSpace(protectedText))
        {
            return string.Empty;
        }

        var fullCipher = Convert.FromBase64String(protectedText);
        using var aesAlg = Aes.Create();
        aesAlg.Key = EncryptionKey;
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