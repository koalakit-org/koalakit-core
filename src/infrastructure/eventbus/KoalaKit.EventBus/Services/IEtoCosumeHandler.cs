using KoalaKit.EventBus.Models;

namespace KoalaKit.EventBus.Services;

public interface IEtoCosumeHandler<in TEto> where TEto : IKoalaEventMessage
{
    Task<bool> HandleAsync(TEto eto, CancellationToken cancellationToken);
}