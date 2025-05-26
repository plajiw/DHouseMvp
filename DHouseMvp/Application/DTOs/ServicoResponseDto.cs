namespace DHouseMvp.Application.DTOs // Ensure this namespace is correct
{
    public class ServicoResponseDto // Ensure this class name is correct
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal? PrecoBase { get; set; }
        public bool Ativo { get; set; }
    }
}