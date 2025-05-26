using System.ComponentModel.DataAnnotations;

namespace DHouseMvp.Application.DTOs // Ensure this namespace
{
    public class ImovelDto // Ensure this class name
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