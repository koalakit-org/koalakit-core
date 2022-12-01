namespace Koala.Authentications
{
    public interface IKoalaIdentityService
    {
        Task Add(IdentityAddParameters parameters);
        Task AddOrUpdate(IdentityAddParameters parameters);
        Task Block(string userId);
        Task Remove(string userId);
    }
}
