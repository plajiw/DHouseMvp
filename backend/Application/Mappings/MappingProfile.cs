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
            CreateMap<ImovelCreateDto, Imovel>();
            CreateMap<Imovel, ImovelResponseDto>();

            // ServicoOferecido Mappings
            CreateMap<ServicoCreateDto, ServicoOferecido>();
            CreateMap<ServicoOferecido, ServicoResponseDto>();
        }
    }
}