namespace Koalakit.Primitives;

public sealed record AccessToken(
    string Token,
    DateTime ExpiresAt,
    string TokenType = "Bearer");
