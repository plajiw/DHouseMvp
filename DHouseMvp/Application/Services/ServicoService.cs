// Application/Services/ServicoService.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DHouseMvp.Application.DTOs;    // DIRETIVA USING ESSENCIAL (verifique se não está duplicada)
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Core.Entities;
using DHouseMvp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DHouseMvp.Application.Services
{
    public class ServicoService : IServicoService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicoService> _logger;

        public ServicoService(ApplicationDbContext ctx, IMapper mapper, ILogger<ServicoService> logger)
        {
            _ctx = ctx;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ServicoResponseDto>> GetAllAsync()
        {
            _logger?.LogInformation("Buscando todos os Serviços");
            return await _ctx.Servicos
                             .AsNoTracking()
                             .ProjectTo<ServicoResponseDto>(_mapper.ConfigurationProvider)
                             .ToListAsync();
        }

        public async Task<ServicoResponseDto> GetByIdAsync(int id)
        {
            _logger?.LogInformation("Buscando Serviço por Id: {Id}", id);
            var entity = await _ctx.Servicos.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null)
            {
                _logger?.LogWarning("Serviço com Id: {Id} não encontrado.", id);
                return null;
            }
            return _mapper.Map<ServicoResponseDto>(entity);
        }

        public async Task<ServicoResponseDto> CreateAsync(ServicoDto dto) // Usa ServicoDto
        {
            _logger?.LogInformation("Criando novo Serviço");
            var entity = _mapper.Map<ServicoOferecido>(dto);
            _ctx.Servicos.Add(entity);
            await _ctx.SaveChangesAsync();
            _logger?.LogInformation("Serviço criado com Id: {Id}", entity.Id);
            return _mapper.Map<ServicoResponseDto>(entity);
        }

        public async Task<ServicoResponseDto> UpdateAsync(int id, ServicoDto dto) // Usa ServicoDto
        {
            _logger?.LogInformation("Atualizando Serviço com Id: {Id}", id);
            var entity = await _ctx.Servicos.FindAsync(id);
            if (entity == null)
            {
                _logger?.LogWarning("Serviço com Id: {Id} não encontrado para atualização.", id);
                return null;
            }
            _mapper.Map(dto, entity);
            await _ctx.SaveChangesAsync();
            _logger?.LogInformation("Serviço com Id: {Id} atualizado.", id);
            return _mapper.Map<ServicoResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger?.LogInformation("Deletando Serviço com Id: {Id}", id);
            var entity = await _ctx.Servicos.FindAsync(id);
            if (entity == null)
            {
                _logger?.LogWarning("Serviço com Id: {Id} não encontrado para deleção.", id);
                return false;
            }
            _ctx.Servicos.Remove(entity);
            await _ctx.SaveChangesAsync();
            _logger?.LogInformation("Serviço com Id: {Id} deletado.", id);
            return true;
        }
    }
}
