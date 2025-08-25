using ApiProposta.DTOs;
using DomainProposta.Entities;
using AutoMapper;

namespace ApiProposta.Mappings
{
    public class PropostaProfile : Profile
    {
        public PropostaProfile()
        {
            CreateMap<Proposta, PropostaDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<PropostaDto, Proposta>(); 
        }
    }
}
