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
    public class ServicoService : IServicoService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMapper _mapper;

        public ServicoService(ApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<List<ServicoOferecido>> GetAllAsync()
            => await _ctx.Servicos.ToListAsync();

        public async Task<ServicoOferecido> CreateAsync(ServicoDto dto)
        {
            var entity = _mapper.Map<ServicoOferecido>(dto);
            _ctx.Servicos.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
    }
}
