// Application/DTOs/ServicoResponseDto.cs
namespace DHouseMvp.Application.DTOs
{
    public class ServicoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal? PrecoBase { get; set; }
        public bool Ativo { get; set; }
    }
}