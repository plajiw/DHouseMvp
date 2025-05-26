// Application/Interfaces/IImovelService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using DHouseMvp.Core.Entities; // Keep this if services sometimes work with entities internally
using DHouseMvp.Application.DTOs;

namespace DHouseMvp.Application.Interfaces
{
    public interface IImovelService
    {
        Task<List<ImovelResponseDto>> GetAllAsync();
        Task<ImovelResponseDto> GetByIdAsync(int id);
        Task<ImovelResponseDto> CreateAsync(ImovelDto dto);
        Task<ImovelResponseDto> UpdateAsync(int id, ImovelDto dto);
        Task<bool> DeleteAsync(int id);
    }
}