namespace DomainContratacao.Interfaces
{
    public interface IEventBus
    {
        Task PublicarAsync<T>(string topico, T mensagem);
    }
}
