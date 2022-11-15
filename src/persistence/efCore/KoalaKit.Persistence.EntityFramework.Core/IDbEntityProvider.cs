using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore
{
    public interface IDbEntityProvider
    {
        void OnModelCreating(ModelBuilder modelBuilder);
    }

    public static class EntityProvidersCollection
    {
        private static readonly List<IDbEntityProvider> Providers = new();
        public static IEnumerable<IDbEntityProvider> List => Providers;

        public static void AddDbEntityProvider(Assembly assembly)
            => Providers.AddRange(assembly.GetTypes()
                .Where(type => !type.IsAbstract && typeof(IDbEntityProvider).IsAssignableFrom(type))
                .AsEnumerable()
                .Select(Activator.CreateInstance)
                .Cast<IDbEntityProvider>()
                .ToList());

    }
}
