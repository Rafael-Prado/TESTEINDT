using DomainProposta.Enums;

namespace DomainProposta.Entities
{
    public  record PropostaMongo
    (
        Guid IdProposta,
        Guid IdCliente,
        Guid IdProduto,
        decimal Valor,
        StatusProposta Status
    );
}
