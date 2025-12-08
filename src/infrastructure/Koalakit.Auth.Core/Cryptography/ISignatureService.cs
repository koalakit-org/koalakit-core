namespace Koalakit.Auth.Core.Cryptography;

/// <summary>
/// Interface for generating and validating HMAC signatures.
/// </summary>
public interface ISignatureService
{
    /// <summary>
    /// Generates a signature for the provided data.
    /// </summary>
    /// <param name="data">The data to sign</param>
    /// <returns>Base64-encoded signature</returns>
    string GenerateSignature(string data);
    
    /// <summary>
    /// Validates a signature against the provided data.
    /// </summary>
    /// <param name="data">The data to validate</param>
    /// <param name="signature">The signature to validate</param>
    /// <returns>True if the signature is valid, false otherwise</returns>
    bool ValidateSignature(string data, string signature);
}

