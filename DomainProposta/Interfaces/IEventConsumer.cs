

namespace DomainProposta.Interfaces
{
    public interface IEventConsumer
    {
        Task ConsumirAsync<T>(Func<T, Task> onMessage, CancellationToken cancellationToken);
    }
}
