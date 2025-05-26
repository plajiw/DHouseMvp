using AutoMapper;
using DHouseMvp.Application.DTOs;
using DHouseMvp.Core.Entities;

namespace DHouseMvp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Imovel Mappings
            CreateMap<ImovelDto, Imovel>();
            CreateMap<Imovel, ImovelResponseDto>();

            // ServicoOferecido Mappings
            CreateMap<ServicoDto, ServicoOferecido>();
            CreateMap<ServicoOferecido, ServicoResponseDto>();
        }
    }
}