// Application/Mappings/MappingProfile.cs
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
            CreateMap<ImovelDto, Imovel>(); // For creating/updating Imovel
            CreateMap<Imovel, ImovelResponseDto>(); // For returning Imovel data

            // ServicoOferecido Mappings
            CreateMap<ServicoDto, ServicoOferecido>(); // For creating/updating ServicoOferecido
            CreateMap<ServicoOferecido, ServicoResponseDto>(); // For returning ServicoOferecido data
        }
    }
}