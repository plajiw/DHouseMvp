using System;

namespace DHouseMvp.Application.DTOs
{
    public class ImovelResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool Publicado { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}