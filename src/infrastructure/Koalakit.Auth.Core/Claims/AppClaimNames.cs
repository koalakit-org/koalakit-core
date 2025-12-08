namespace Koalakit.Auth.Core.Claims;

/// <summary>
/// Provides built-in claim names and extensibility for custom claims.
/// </summary>
public static class AppClaimNames
{
    // Built-in claims
    public const string Scope = "scope";
    
    // Registry for custom claims
    private static readonly HashSet<string> _customClaims = new(StringComparer.OrdinalIgnoreCase);
    private static readonly List<IClaimProvider> _claimProviders = new();
    private static readonly object _lock = new();

    /// <summary>
    /// Registers a custom claim name. Thread-safe.
    /// </summary>
    /// <param name="claimName">The claim name to register</param>
    /// <returns>True if the claim was added, false if it already existed</returns>
    public static bool RegisterClaim(string claimName)
    {
        if (string.IsNullOrWhiteSpace(claimName))
        {
            throw new ArgumentException("Claim name cannot be null or empty", nameof(claimName));
        }

        lock (_lock)
        {
            return _customClaims.Add(claimName);
        }
    }

    /// <summary>
    /// Registers multiple custom claim names. Thread-safe.
    /// </summary>
    /// <param name="claimNames">The collection of claim names to register</param>
    public static void RegisterClaims(IEnumerable<string> claimNames)
    {
        if (claimNames == null)
        {
            throw new ArgumentNullException(nameof(claimNames));
        }

        foreach (var claim in claimNames)
        {
            RegisterClaim(claim);
        }
    }

    /// <summary>
    /// Registers a claim provider that can supply claims dynamically.
    /// </summary>
    /// <param name="provider">The claim provider to register</param>
    public static void RegisterClaimProvider(IClaimProvider provider)
    {
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        lock (_lock)
        {
            _claimProviders.Add(provider);
        }
    }

    /// <summary>
    /// Unregisters a custom claim. Thread-safe.
    /// </summary>
    /// <param name="claimName">The claim name to unregister</param>
    /// <returns>True if the claim was removed, false if it didn't exist</returns>
    public static bool UnregisterClaim(string claimName)
    {
        if (string.IsNullOrWhiteSpace(claimName))
        {
            return false;
        }

        lock (_lock)
        {
            return _customClaims.Remove(claimName);
        }
    }

    /// <summary>
    /// Clears all custom registered claims and providers. Thread-safe.
    /// Useful for testing.
    /// </summary>
    public static void ClearCustomClaims()
    {
        lock (_lock)
        {
            _customClaims.Clear();
            _claimProviders.Clear();
        }
    }

    /// <summary>
    /// Gets all built-in claims defined as constants in this class.
    /// </summary>
    /// <returns>Collection of built-in claim names</returns>
    public static IEnumerable<string> ListBuiltInClaims()
    {
        return typeof(AppClaimNames)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
            .Where(field => field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string))
            .Select(field => field.GetValue(null) as string ?? string.Empty)
            .Where(claim => !string.IsNullOrEmpty(claim));
    }

    /// <summary>
    /// Gets all custom registered claims.
    /// </summary>
    /// <returns>Collection of custom claim names</returns>
    public static IEnumerable<string> ListCustomClaims()
    {
        lock (_lock)
        {
            var fromRegistry = _customClaims.ToList();
            var fromProviders = _claimProviders
                .SelectMany(p => p.GetClaims())
                .Where(c => !string.IsNullOrWhiteSpace(c));
            
            return fromRegistry.Concat(fromProviders).Distinct(StringComparer.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Lists all claims (both built-in and custom registered).
    /// </summary>
    /// <returns>Collection of all claim names</returns>
    public static IEnumerable<string> ListAllClaims()
    {
        var builtIn = ListBuiltInClaims();
        var custom = ListCustomClaims();
        return builtIn.Concat(custom).Distinct(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks if a claim is valid (exists in built-in or custom claims).
    /// </summary>
    /// <param name="claim">The claim name to validate</param>
    /// <returns>True if the claim is valid, false otherwise</returns>
    public static bool IsValidClaim(string? claim)
    {
        if (string.IsNullOrEmpty(claim))
        {
            return false;
        }
        
        return ListAllClaims().Contains(claim, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks if a claim is a built-in claim.
    /// </summary>
    /// <param name="claim">The claim name to check</param>
    /// <returns>True if the claim is built-in, false otherwise</returns>
    public static bool IsBuiltInClaim(string? claim)
    {
        if (string.IsNullOrEmpty(claim))
        {
            return false;
        }
        
        return ListBuiltInClaims().Contains(claim, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks if a claim is a custom registered claim.
    /// </summary>
    /// <param name="claim">The claim name to check</param>
    /// <returns>True if the claim is custom registered, false otherwise</returns>
    public static bool IsCustomClaim(string? claim)
    {
        if (string.IsNullOrEmpty(claim))
        {
            return false;
        }

        return ListCustomClaims().Contains(claim, StringComparer.OrdinalIgnoreCase);
    }
}

