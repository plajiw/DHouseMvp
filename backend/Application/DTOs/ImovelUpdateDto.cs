using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Application.DTOs
{
    public class ImovelUpdateDto
    {
        [MaxLength(100)]
        public string? Titulo { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Preco { get; set; }
    }

}
