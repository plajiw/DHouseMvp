using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Application.DTOs; // THIS LINE MUST BE PRESENT

namespace DHouseMvp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImoveisController : ControllerBase
    {
        private readonly IImovelService _service;

        public ImoveisController(IImovelService service)
            => _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ImovelResponseDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImovelResponseDto>> GetById(int id)
        {
            var imovel = await _service.GetByIdAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }
            return Ok(imovel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ImovelResponseDto>> Create([FromBody] ImovelDto dto) // Error here if ImovelDto not found
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdImovel = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdImovel.Id }, createdImovel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImovelResponseDto>> Update(int id, [FromBody] ImovelDto dto) // Error here if ImovelDto not found
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedImovel = await _service.UpdateAsync(id, dto);
            if (updatedImovel == null)
            {
                return NotFound();
            }
            return Ok(updatedImovel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}