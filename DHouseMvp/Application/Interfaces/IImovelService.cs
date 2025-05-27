using System.Collections.Generic;
using System.Threading.Tasks;
using DHouseMvp.Application.DTOs; // Crucial: Add this using directive

namespace DHouseMvp.Application.Interfaces
{
    public interface IImovelService
    {
        Task<List<ImovelResponseDto>> GetAllAsync();
        Task<ImovelResponseDto> GetByIdAsync(int id);
        Task<ImovelResponseDto> CreateAsync(ImovelCreateDto dto); // Uses ImovelDto
        Task<ImovelResponseDto> UpdateAsync(int id, ImovelCreateDto dto); // Uses ImovelDto
        Task<bool> DeleteAsync(int id);
    }
}