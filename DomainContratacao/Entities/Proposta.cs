using DomainContratacao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainContratacao.Entities
{
    public class Proposta
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdProduto { get; set; }
        public decimal Valor { get; set; }
        public StatusProposta Status { get; set; }
    }
}
