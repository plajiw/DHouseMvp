using System.Collections.Generic;
using System.Threading.Tasks;
using DHouseMvp.Core.Entities;
using DHouseMvp.Application.DTOs;

namespace DHouseMvp.Application.Interfaces
{
    public interface IServicoService
    {
        Task<List<ServicoOferecido>> GetAllAsync();
        Task<ServicoOferecido> CreateAsync(ServicoDto dto);
        // Você pode depois adicionar UpdateAsync, DeleteAsync etc.
    }
}
