using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

// 🚩 Adicione estes:
using DHouseMvp.Application.Interfaces;
using DHouseMvp.Application.DTOs;
using DHouseMvp.Core.Entities;

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
        public async Task<ActionResult<List<Imovel>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<ActionResult<Imovel>> Create([FromBody] ImovelDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
