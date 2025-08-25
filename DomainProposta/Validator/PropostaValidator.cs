using DomainProposta.Entities;
using FluentValidation;

namespace DomainProposta.Validator
{
    public class PropostaValidator : AbstractValidator<Proposta>
    {
        public PropostaValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id do pedido não pode ser vazio.");

            RuleFor(x => x.IdCliente)
                .NotEmpty().WithMessage("O Id do cliente não pode ser vazio.");

            RuleFor(x => x.IdProduto)
                .NotEmpty().WithMessage("O Id do Produto não pode ser vazio.");
                
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status não pode ser vazio.");

            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("Valor não pode ser vazio.");
        }
    }
}
