// Application/Services/ImovelService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DHouseMvp.Application.DTOs;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Core.Entities;
using DHouseMvp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Required for ProjectTo
using DHouseMvp.Application.DTOs;

namespace DHouseMvp.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _mapper;

        public ImovelService(ApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<List<ImovelResponseDto>> GetAllAsync()
            => await _ctx.Imoveis
                         .AsNoTracking() // Good practice for read-only queries
                         .ProjectTo<ImovelResponseDto>(_mapper.ConfigurationProvider)
                         .ToListAsync();

        public async Task<ImovelResponseDto> GetByIdAsync(int id)
        {
            var entity = await _ctx.Imoveis.FindAsync(id);
            return _mapper.Map<ImovelResponseDto>(entity); // AutoMapper handles null to null mapping
        }

        public async Task<ImovelResponseDto> CreateAsync(ImovelDto dto)
        {
            var entity = _mapper.Map<Imovel>(dto);
            _ctx.Imoveis.Add(entity);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<ImovelResponseDto>(entity);
        }

        public async Task<ImovelResponseDto> UpdateAsync(int id, ImovelDto dto)
        {
            var entity = await _ctx.Imoveis.FindAsync(id);
            if (entity == null)
            {
                return null; // Or throw a NotFoundException
            }

            _mapper.Map(dto, entity); // Update existing entity
            await _ctx.SaveChangesAsync();
            return _mapper.Map<ImovelResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _ctx.Imoveis.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _ctx.Imoveis.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}