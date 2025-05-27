// Application/DTOs/ServicoUpdateDto.cs
using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Application.DTOs
{
    public class ServicoUpdateDto
    {
        [MaxLength(255, ErrorMessage = "O nome não pode ultrapassar 255 caracteres.")]
        public string? Nome { get; set; }

        [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 500 caracteres.")]
        public string? Descricao { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
        public decimal? PrecoBase { get; set; }

        public bool? Ativo { get; set; }
    }
}
