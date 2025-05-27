using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Application.DTOs
{
    public class ImovelCreateDto
    {

        [Required(ErrorMessage = "O t�tulo do im�veis � obrigat�rio.")]
        [MaxLength(255, ErrorMessage = "O t�tulo n�o pode ultrapassar 255 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "A descri��o n�o pode ultrapassar 500 caracteres.")]
        public string? Descricao { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O pre�o deve ser um valor positivo.")]
        public decimal Preco { get; set; }
    }
}