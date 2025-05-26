// Application/Interfaces/IImovelService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using DHouseMvp.Core.Entities;
using DHouseMvp.Application.DTOs;

namespace DHouseMvp.Application.Interfaces
{
    public interface IImovelService
    {
        Task<List<Imovel>> GetAllAsync();
        Task<Imovel> GetByIdAsync(int id);
        Task<Imovel> CreateAsync(ImovelDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
