namespace Koalakit.Auth.Core.Cryptography;

/// <summary>
/// Interface for encrypting and decrypting sensitive data.
/// </summary>
public interface ITokenizerService
{
    /// <summary>
    /// Encrypts the provided plain text.
    /// </summary>
    /// <param name="plainText">The text to encrypt</param>
    /// <returns>Base64-encoded encrypted text</returns>
    string Tokenize(string? plainText);
    
    /// <summary>
    /// Decrypts the provided protected text.
    /// </summary>
    /// <param name="protectedText">The encrypted text to decrypt</param>
    /// <returns>Decrypted plain text</returns>
    string Detokenize(string protectedText);
}

