namespace Koalakit.Auth.Core.Configuration;

/// <summary>
/// Configuration options for Koalakit.Auth.Core services.
/// </summary>
public class AuthCoreOptions
{
    /// <summary>
    /// Gets or sets the secret key used for HMAC signature generation.
    /// </summary>
    public string SignatureSecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the encryption key used for tokenization (must be 32 bytes when UTF-8 encoded).
    /// </summary>
    public string EncryptionKey { get; set; } = string.Empty;

    /// <summary>
    /// Validates the configuration options.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when configuration is invalid</exception>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(SignatureSecretKey))
        {
            throw new InvalidOperationException("SignatureSecretKey cannot be null or empty");
        }

        if (string.IsNullOrWhiteSpace(EncryptionKey))
        {
            throw new InvalidOperationException("EncryptionKey cannot be null or empty");
        }

        var keyBytes = System.Text.Encoding.UTF8.GetBytes(EncryptionKey);
        if (keyBytes.Length != 32)
        {
            throw new InvalidOperationException("EncryptionKey must be exactly 32 bytes when UTF-8 encoded");
        }
    }
}

