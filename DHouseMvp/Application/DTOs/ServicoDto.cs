// Application/DTOs/ServicoDto.cs
using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Application.DTOs // Certifique-se que este namespace está correto
{
    public class ServicoDto // Certifique-se que o nome da classe está correto
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PrecoBase { get; set; }
    }
}
