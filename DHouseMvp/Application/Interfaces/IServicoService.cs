// Application/Interfaces/IServicoService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using DHouseMvp.Core.Entities; // Keep this
using DHouseMvp.Application.DTOs;

namespace DHouseMvp.Application.Interfaces
{
    public interface IServicoService
    {
        Task<List<ServicoResponseDto>> GetAllAsync();
        Task<ServicoResponseDto> GetByIdAsync(int id);
        Task<ServicoResponseDto> CreateAsync(ServicoDto dto);
        Task<ServicoResponseDto> UpdateAsync(int id, ServicoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}