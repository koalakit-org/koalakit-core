namespace Koalakit.Auth.Core.Claims;

/// <summary>
/// Interface for providing custom claims to the AppClaimNames registry.
/// </summary>
public interface IClaimProvider
{
    /// <summary>
    /// Gets the collection of claim names provided by this provider.
    /// </summary>
    IEnumerable<string> GetClaims();
}

