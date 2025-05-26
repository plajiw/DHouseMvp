using System;
using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Core.Entities
{
    public class Imovel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Titulo { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Preco { get; set; }

        public bool Publicado { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
