using System.Collections.Generic;
using System.Linq; // For ProjectTo
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions; // For ProjectTo
using DHouseMvp.Application.DTOs;    // Crucial: Ensure this is present and NOT duplicated
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Core.Entities;
using DHouseMvp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Optional: For logging

namespace DHouseMvp.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ILogger<ImovelService> _logger; // Optional

        public ImovelService(ApplicationDbContext ctx, IMapper mapper, ILogger<ImovelService> logger) // Optional: Add logger
        {
            _ctx = ctx;
            _mapper = mapper;
            _logger = logger; // Optional
        }

        public async Task<List<ImovelResponseDto>> GetAllAsync()
        {
            _logger?.LogInformation("Getting all Imoveis");
            return await _ctx.Imoveis
                             .AsNoTracking()
                             .ProjectTo<ImovelResponseDto>(_mapper.ConfigurationProvider)
                             .ToListAsync();
        }

        public async Task<ImovelResponseDto> GetByIdAsync(int id)
        {
            _logger?.LogInformation("Getting Imovel by Id: {Id}", id);
            var entity = await _ctx.Imoveis.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                _logger?.LogWarning("Imovel with Id: {Id} not found.", id);
                return null;
            }
            return _mapper.Map<ImovelResponseDto>(entity);
        }

        public async Task<ImovelResponseDto> CreateAsync(ImovelDto dto) // Uses ImovelDto
        {
            _logger?.LogInformation("Creating new Imovel");
            var entity = _mapper.Map<Imovel>(dto);
            _ctx.Imoveis.Add(entity);
            await _ctx.SaveChangesAsync();
            _logger?.LogInformation("Imovel created with Id: {Id}", entity.Id);
            return _mapper.Map<ImovelResponseDto>(entity);
        }

        public async Task<ImovelResponseDto> UpdateAsync(int id, ImovelDto dto) // Uses ImovelDto
        {
            _logger?.LogInformation("Updating Imovel with Id: {Id}", id);
            var entity = await _ctx.Imoveis.FindAsync(id);
            if (entity == null)
            {
                _logger?.LogWarning("Imovel with Id: {Id} not found for update.", id);
                return null;
            }
            _mapper.Map(dto, entity);
            await _ctx.SaveChangesAsync();
            _logger?.LogInformation("Imovel with Id: {Id} updated.", id);
            return _mapper.Map<ImovelResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger?.LogInformation("Deleting Imovel with Id: {Id}", id);
            var entity = await _ctx.Imoveis.FindAsync(id);
            if (entity == null)
            {
                _logger?.LogWarning("Imovel with Id: {Id} not found for deletion.", id);
                return false;
            }
            _ctx.Imoveis.Remove(entity);
            await _ctx.SaveChangesAsync();
            _logger?.LogInformation("Imovel with Id: {Id} deleted.", id);
            return true;
        }
    }
}