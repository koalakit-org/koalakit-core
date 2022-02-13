using System.Reflection;
using KoalaKit.Options;

namespace KoalaKit.Extensions
{
    public static class KoalaOptionsBuilderExtensions
    {
        public static KoalaOptionsBuilder AddModules(this KoalaOptionsBuilder builder, IEnumerable<Assembly> assemblies)
        {
            ///TODO: fetch the modules and register all services 
            return builder;
        }
    }
}
