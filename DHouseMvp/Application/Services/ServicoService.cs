// Application/Services/ServicoService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DHouseMvp.Application.DTOs;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Core.Entities;
using DHouseMvp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Required for ProjectTo

namespace DHouseMvp.Application.Services
{
    public class ServicoService : IServicoService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _mapper;

        public ServicoService(ApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<List<ServicoResponseDto>> GetAllAsync()
            => await _ctx.Servicos
                         .AsNoTracking()
                         .ProjectTo<ServicoResponseDto>(_mapper.ConfigurationProvider)
                         .ToListAsync();

        public async Task<ServicoResponseDto> GetByIdAsync(int id)
        {
            var entity = await _ctx.Servicos.FindAsync(id);
            return _mapper.Map<ServicoResponseDto>(entity);
        }

        public async Task<ServicoResponseDto> CreateAsync(ServicoDto dto)
        {
            var entity = _mapper.Map<ServicoOferecido>(dto);
            _ctx.Servicos.Add(entity);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<ServicoResponseDto>(entity);
        }

        public async Task<ServicoResponseDto> UpdateAsync(int id, ServicoDto dto)
        {
            var entity = await _ctx.Servicos.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            _mapper.Map(dto, entity);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<ServicoResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _ctx.Servicos.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            _ctx.Servicos.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}