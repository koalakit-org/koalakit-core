using KoalaKit.Modules;

namespace KoalaKit.Options
{
    public class KoalaOptions
    {
        internal KoalaOptions() { }
        internal List<IKoalaModule> KoalaModules { get; } = new();
    }
}