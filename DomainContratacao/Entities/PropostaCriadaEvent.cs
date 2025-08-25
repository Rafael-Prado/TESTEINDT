
using DomainContratacao.Enums;

namespace DomainContratacao.Entities
{
    public class PropostaCriadaEvent
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdProduto { get; set; }
        public decimal Valor { get; set; }
        public StatusProposta Status { get; set; }
    }
}
