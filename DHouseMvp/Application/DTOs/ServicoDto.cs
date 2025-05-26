using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Application.DTOs
{
    public class ServicoDto
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PrecoBase { get; set; }
    }
}
