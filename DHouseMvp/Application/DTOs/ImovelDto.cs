// Application/DTOs/ImovelDto.cs
using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Application.DTOs
{
    public class ImovelDto
    {
        [Required, MaxLength(100)]
        public string Titulo { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Preco { get; set; }

        public bool Publicado { get; set; }
    }
}
