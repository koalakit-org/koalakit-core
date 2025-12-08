namespace Koalakit.Auth.Core.Models;

/// <summary>
/// Represents an access token with expiration information.
/// </summary>
/// <param name="Token">The token value</param>
/// <param name="ExpiresAt">The date and time when the token expires</param>
/// <param name="TokenType">The type of token (default is "Bearer")</param>
public sealed record AccessToken(
    string Token,
    DateTime ExpiresAt,
    string TokenType = "Bearer");

