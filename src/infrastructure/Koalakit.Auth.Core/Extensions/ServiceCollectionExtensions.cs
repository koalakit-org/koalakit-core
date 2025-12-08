using Koalakit.Auth.Core.Configuration;
using Koalakit.Auth.Core.Cryptography;
using Microsoft.Extensions.DependencyInjection;

namespace Koalakit.Auth.Core.Extensions;

/// <summary>
/// Extension methods for configuring Koalakit.Auth.Core services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Koalakit.Auth.Core services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configure">Configuration action for AuthCoreOptions</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddKoalakitAuthCore(
        this IServiceCollection services,
        Action<AuthCoreOptions> configure)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure == null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var options = new AuthCoreOptions();
        configure(options);
        options.Validate();

        // Register signature service
        services.AddSingleton<ISignatureService>(sp => 
            new SignatureService(options.SignatureSecretKey));
        
        // Register tokenizer service
        services.AddSingleton<ITokenizerService>(sp => 
            new TokenizerService(options.EncryptionKey));

        return services;
    }

    /// <summary>
    /// Adds Koalakit.Auth.Core services to the specified <see cref="IServiceCollection"/>
    /// using the provided options instance.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="options">The configured AuthCoreOptions instance</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddKoalakitAuthCore(
        this IServiceCollection services,
        AuthCoreOptions options)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        options.Validate();

        // Register signature service
        services.AddSingleton<ISignatureService>(sp => 
            new SignatureService(options.SignatureSecretKey));
        
        // Register tokenizer service
        services.AddSingleton<ITokenizerService>(sp => 
            new TokenizerService(options.EncryptionKey));

        return services;
    }
}

