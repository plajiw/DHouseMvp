using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DHouseMvp.Application.DTOs;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Core.Entities;
using DHouseMvp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Imovel>> GetAllAsync()
            => await _ctx.Imoveis.ToListAsync();

        public async Task<Imovel> GetByIdAsync(int id)
            => await _ctx.Imoveis.FindAsync(id);

        public async Task<Imovel> CreateAsync(ImovelDto dto)
        {
            var entity = _mapper.Map<Imovel>(dto);
            _ctx.Imoveis.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _ctx.Imoveis.FindAsync(id);
            if (entity == null) return false;
            _ctx.Imoveis.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
