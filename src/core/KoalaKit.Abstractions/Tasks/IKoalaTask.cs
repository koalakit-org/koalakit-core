namespace KoalaKit.Tasks
{
    public interface IKoalaTask
    {
        int Order { get; }
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
