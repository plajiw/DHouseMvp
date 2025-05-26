// Application/Interfaces/IServicoService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using DHouseMvp.Application.DTOs; // DIRETIVA USING ESSENCIAL

namespace DHouseMvp.Application.Interfaces
{
    public interface IServicoService
    {
        Task<List<ServicoResponseDto>> GetAllAsync();
        Task<ServicoResponseDto> GetByIdAsync(int id);
        Task<ServicoResponseDto> CreateAsync(ServicoDto dto); // Usa ServicoDto
        Task<ServicoResponseDto> UpdateAsync(int id, ServicoDto dto); // Usa ServicoDto
        Task<bool> DeleteAsync(int id);
    }
}
