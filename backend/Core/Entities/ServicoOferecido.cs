// Core/Entities/ServicoOferecido.cs
using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Core.Entities
{
    public class ServicoOferecido
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PrecoBase { get; set; }

        public bool Ativo { get; set; } = true;

        // public bool Removido { get; set; } = false;
    }
}