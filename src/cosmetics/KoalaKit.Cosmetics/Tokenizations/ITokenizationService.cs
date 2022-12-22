namespace KoalaKit.Cosmetics
{
    public interface ITokenizationService
    {
        public TData? GetData<TData>(string token);
        public string Tokenize<TData>(TData data);
    }
}
