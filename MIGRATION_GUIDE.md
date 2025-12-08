# Migration Guide: Auth Package Extraction

This document summarizes the extraction of authentication, authorization, and claims functionality from `Koalakit.Primitives` into a new package: `Koalakit.Auth.Core`.

## What Was Created

### Koalakit.Auth.Core
**Location**: `src/infrastructure/Koalakit.Auth.Core/`

A standalone package containing core authentication primitives with no ASP.NET dependencies:

#### Structure
```
Koalakit.Auth.Core/
├── Claims/
│   ├── AppClaimNames.cs          (Enhanced with extensibility)
│   └── IClaimProvider.cs         (New interface for claim providers)
├── Cryptography/
│   ├── ISignatureService.cs      (New interface)
│   ├── SignatureService.cs       (Refactored to instance-based)
│   ├── ITokenizerService.cs      (New interface)
│   └── TokenizerService.cs       (Refactored to instance-based)
├── Models/
│   └── AccessToken.cs            (Moved from Primitives)
├── Configuration/
│   └── AuthCoreOptions.cs        (New configuration class)
├── Extensions/
│   └── ServiceCollectionExtensions.cs  (New DI extensions)
├── README.md                     (Comprehensive documentation)
└── Koalakit.Auth.Core.csproj
```

#### Key Features
- ✅ Claims registration and validation
- ✅ Claim providers for structured management
- ✅ Thread-safe operations
- ✅ HMAC-SHA256 signatures
- ✅ AES-256 encryption/decryption
- ✅ Dependency injection support
- ✅ Interface-based design for testability

## What Was Removed from Koalakit.Primitives

The following files were **deleted** from `Koalakit.Primitives`:
- ❌ `AppClaimNames.cs` → Moved to `Koalakit.Auth.Core`
- ❌ `AccessToken.cs` → Moved to `Koalakit.Auth.Core`
- ❌ `PolicyNames.cs` → Deleted (was only used for ASP.NET Core integration)
- ❌ `Tokenizations/SignatureService.cs` → Moved to `Koalakit.Auth.Core`
- ❌ `Tokenizations/TokenizerService.cs` → Moved to `Koalakit.Auth.Core`

## Breaking Changes

### Namespace Changes

| Old (Koalakit.Primitives) | New |
|---------------------------|-----|
| `Koalakit.Primitives.AppClaimNames` | `Koalakit.Auth.Core.Claims.AppClaimNames` |
| `Koalakit.Primitives.AccessToken` | `Koalakit.Auth.Core.Models.AccessToken` |
| `Koalakit.Primitives.Tokenizations.SignatureService` | `Koalakit.Auth.Core.Cryptography.SignatureService` |
| `Koalakit.Primitives.Tokenizations.TokenizerService` | `Koalakit.Auth.Core.Cryptography.TokenizerService` |

### API Changes

#### SignatureService (Static → Instance-based with DI)

**Before:**
```csharp
using Koalakit.Primitives.Tokenizations;

var signature = SignatureService.GenerateSignature(data);
var isValid = SignatureService.ValidateSignature(data, signature);
```

**After:**
```csharp
using Koalakit.Auth.Core.Cryptography;

// In Startup/Program.cs
builder.Services.AddKoalakitAuthCore(options =>
{
    options.SignatureSecretKey = "your-secret-key";
    options.EncryptionKey = "12345678901234567890123456789012";
});

// In your service
public class MyService
{
    private readonly ISignatureService _signatureService;
    
    public MyService(ISignatureService signatureService)
    {
        _signatureService = signatureService;
    }
    
    public void DoWork()
    {
        var signature = _signatureService.GenerateSignature(data);
        var isValid = _signatureService.ValidateSignature(data, signature);
    }
}
```

#### TokenizerService (Extension Methods → Instance-based with DI)

**Before:**
```csharp
using Koalakit.Primitives.Tokenizations;

var encrypted = plainText.Tokenize();
var decrypted = encrypted.Detokenize();
```

**After:**
```csharp
using Koalakit.Auth.Core.Cryptography;

// Inject via DI
public class MyService
{
    private readonly ITokenizerService _tokenizerService;
    
    public MyService(ITokenizerService tokenizerService)
    {
        _tokenizerService = tokenizerService;
    }
    
    public void DoWork()
    {
        var encrypted = _tokenizerService.Tokenize(plainText);
        var decrypted = _tokenizerService.Detokenize(encrypted);
    }
}
```

#### AppClaimNames (Enhanced, but backward compatible)

**Before:**
```csharp
using Koalakit.Primitives;

var allClaims = AppClaimNames.ListAllClaims();
var isValid = AppClaimNames.IsValidClaim("scope");
```

**After (same API + new features):**
```csharp
using Koalakit.Auth.Core.Claims;

// Same as before
var allClaims = AppClaimNames.ListAllClaims();
var isValid = AppClaimNames.IsValidClaim("scope");

// NEW: Register custom claims
AppClaimNames.RegisterClaim("user:read");
AppClaimNames.RegisterClaims(new[] { "user:read", "user:write" });

// NEW: Use claim providers
AppClaimNames.RegisterClaimProvider(new MyClaimProvider());
```

## How to Update Your Code

### Step 1: Update Package References

Remove dependency on `Koalakit.Primitives` for auth features and add new packages:

```xml
<ItemGroup>
  <!-- Add new package -->
  <PackageReference Include="Koalakit.Auth.Core" Version="1.0.0" />
</ItemGroup>
```

### Step 2: Update Using Statements

Find and replace namespaces:
- `using Koalakit.Primitives;` → Update based on what you're using
- Add `using Koalakit.Auth.Core.Claims;` for claims
- Add `using Koalakit.Auth.Core.Cryptography;` for crypto services
- Add `using Koalakit.Auth.Core.Models;` for AccessToken

### Step 3: Configure Services in Program.cs

```csharp
using Koalakit.Auth.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Auth.Core services
builder.Services.AddKoalakitAuthCore(options =>
{
    options.SignatureSecretKey = builder.Configuration["Auth:SignatureKey"]!;
    options.EncryptionKey = builder.Configuration["Auth:EncryptionKey"]!;
});
```

### Step 4: Update Service Constructors

Add dependencies via constructor injection:

```csharp
public class MyService
{
    private readonly ISignatureService _signatureService;
    private readonly ITokenizerService _tokenizerService;
    
    public MyService(
        ISignatureService signatureService,
        ITokenizerService tokenizerService)
    {
        _signatureService = signatureService;
        _tokenizerService = tokenizerService;
    }
}
```

### Step 5: Update Configuration

Add to `appsettings.json`:

```json
{
  "Auth": {
    "SignatureKey": "your-signature-secret-key-here",
    "EncryptionKey": "12345678901234567890123456789012"
  }
}
```

## Benefits of the New Architecture

1. **Separation of Concerns**: Auth primitives separated from general primitives
2. **Dependency Injection**: First-class DI support with interfaces
3. **Testability**: Interface-based design makes unit testing easier
4. **Extensibility**: Claims system supports runtime registration and providers
5. **Security**: No hardcoded secrets, all configuration-based
6. **Thread Safety**: All operations are thread-safe
7. **Focused Package**: Single-purpose package for authentication primitives

## Next Steps

1. **Update your projects** to reference the new packages
2. **Update namespaces** throughout your codebase
3. **Configure services** in your Program.cs/Startup.cs
4. **Test thoroughly** to ensure everything works as expected
5. **Remove** the Koalakit.Primitives dependency if you no longer need it for other features

## Support

For questions or issues during migration:
- Check the README files in each package
- Review the example code in this guide
- Open an issue in the repository

## Summary

✅ **All todos completed**
✅ **No linter errors**
✅ **Full documentation provided**
✅ **Ready for use**

The new packages provide a clean, testable, and extensible architecture for authentication and authorization in Koalakit applications.

