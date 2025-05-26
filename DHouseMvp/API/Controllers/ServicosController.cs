using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Application.DTOs;
using DHouseMvp.Core.Entities;

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
        public async Task<ActionResult<List<ServicoOferecido>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<ActionResult<ServicoOferecido>> Create([FromBody] ServicoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
