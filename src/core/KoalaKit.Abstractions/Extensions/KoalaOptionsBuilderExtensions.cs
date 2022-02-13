using System.Reflection;
using KoalaKit.Modules;
using KoalaKit.Options;

namespace KoalaKit.Extensions
{
    public static class KoalaOptionsBuilderExtensions
    {
        public static KoalaOptionsBuilder AddModules(this KoalaOptionsBuilder builder, IEnumerable<Type> assemblyMarkerTypes)
            => AddModules(builder, assemblyMarkerTypes.Select(x => x.Assembly).Distinct());

        public static KoalaOptionsBuilder AddModules(this KoalaOptionsBuilder builder, IEnumerable<Assembly> assemblies)
        {

            var modulesQuery = from assembly in assemblies
                from type in assembly.GetExportedTypes()
            where type.IsClass && ! type.IsAbstract && typeof(IKoalaModule).IsAssignableFrom(type)
            select type;


            var modules = modulesQuery.ToList();

            var instances = modules
                .Select(module => (IKoalaModule?) Activator.CreateInstance(module, null))
                .Where(instance => instance != null);

            foreach (var instance in instances)
            {
                if (instance == null) continue;
                instance.ConfigureKoala(builder);
                builder.Options.KoalaModules.Add(instance);
            }

            return builder;
        }
    }
}
