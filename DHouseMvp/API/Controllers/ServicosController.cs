// API/Controllers/ServicosController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Application.DTOs; // DIRETIVA USING ESSENCIAL
using Microsoft.AspNetCore.Http;    // Para StatusCodes

// Remova a linha abaixo se não estiver usando entidades diretamente aqui
// using DHouseMvp.Core.Entities;

namespace DHouseMvp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosController : ControllerBase
    {
        private readonly IServicoService _service;

        public ServicosController(IServicoService service)
            => _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ServicoResponseDto>))]
        public async Task<ActionResult<List<ServicoResponseDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServicoResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServicoResponseDto>> GetById(int id)
        {
            var servico = await _service.GetByIdAsync(id);
            if (servico == null)
            {
                return NotFound($"Serviço com ID {id} não encontrado.");
            }
            return Ok(servico);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ServicoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServicoResponseDto>> Create([FromBody] ServicoDto dto) // Usa ServicoDto
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdServico = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdServico.Id }, createdServico);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServicoResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServicoResponseDto>> Update(int id, [FromBody] ServicoDto dto) // Usa ServicoDto
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedServico = await _service.UpdateAsync(id, dto);
            if (updatedServico == null)
            {
                return NotFound($"Serviço com ID {id} não encontrado.");
            }
            return Ok(updatedServico);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Serviço com ID {id} não encontrado.");
            }
            return NoContent();
        }
    }
}