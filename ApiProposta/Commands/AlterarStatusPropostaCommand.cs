using DomainProposta.Enums;
using MediatR;

namespace ApiProposta.Commands
{
    public record AlterarStatusPropostaCommand(Guid PropostaId, StatusProposta Status) : IRequest<Result>;

    public class Result
    {
        public bool Sucesso { get; set; }
        public string Erro { get; set; }
        public string TipoErro { get; set; }
        public Guid IdProposta { get; set; }

        public static Result Ok(Guid idproposta) => new() { Sucesso = true, IdProposta= idproposta };
        public static Result Fail(string erro, string tipoErro) => new() { Sucesso = false, Erro = erro, TipoErro = tipoErro };
    }
}
