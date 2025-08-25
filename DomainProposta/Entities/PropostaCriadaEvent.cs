
namespace DomainProposta.Entities
{
    public class PropostaCriadaEvent
    {
        public Guid Id { get; set; }
        public Guid Cliente { get; set; }
        public Guid Produto { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
    }
}
