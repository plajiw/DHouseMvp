// Application/DTOs/ServicoDto.cs
using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Application.DTOs
{
    public class ServicoCreateDto
    {
        [Required(ErrorMessage = "O título do imóveis é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O título não pode ultrapassar 255 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "A descrição não pode ultrapassar 500 caracteres.")]
        public string? Descricao { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
        public decimal? PrecoBase { get; set; }
    }
}