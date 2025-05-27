using Microsoft.EntityFrameworkCore;
using DHouseMvp.Core.Entities;

namespace DHouseMvp.Infrastructure.Data
{
    // 'public class ApplicationDbContext : DbContext'
    // Declara uma classe pública chamada ApplicationDbContext.
    // O ':' indica que ApplicationDbContext "herda de" DbContext.
    // DbContext é uma classe fundamental do Entity Framework Core. Ela representa uma sessão
    // com o banco de dados e permite que você consulte e salve instâncias de suas entidades.
    // ApplicationDbContext é a sua "ponte" personalizada para o banco de dados.
    public class ApplicationDbContext : DbContext
    {
        // PROPRIEDADES DbSet<T>:
        // Um DbSet<TEntity> representa uma coleção de todas as entidades de um determinado tipo
        // no contexto ou que podem ser consultadas a partir do banco de dados.
        // Essencialmente, cada DbSet<T> mapeia para uma tabela no seu banco de dados.

        // 'public DbSet<Imovel> Imoveis { get; set; }'
        // Declara uma propriedade chamada Imoveis do tipo DbSet<Imovel>.
        // Isso informa ao Entity Framework Core que você tem uma entidade chamada Imovel
        // (definida em Core/Entities/Imovel.cs) e que você quer poder consultá-la e salvá-la.
        // O EF Core, por convenção, criará uma tabela chamada "Imoveis" no banco de dados.
        public DbSet<Imovel> Imoveis { get; set; }

        // 'public DbSet<ServicoOferecido> Servicos { get; set; }'
        // Similarmente, isso declara uma coleção para a sua entidade ServicoOferecido,
        // que será mapeada para uma tabela "Servicos".
        public DbSet<ServicoOferecido> Servicos { get; set; }

        // CONSTRUTOR:
        // 'public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)'
        // Este é o construtor da classe ApplicationDbContext.
        // Ele aceita um parâmetro 'options' do tipo DbContextOptions<ApplicationDbContext>.
        // Estas 'options' são configuradas no arquivo Program.cs (ou Startup.cs em versões mais antigas do .NET)
        // e contêm informações importantes como qual provedor de banco de dados usar (SQLite, PostgreSQL, SQL Server, etc.)
        // e a string de conexão para o banco de dados.
        // Este é um exemplo de Injeção de Dependência: o ApplicationDbContext não cria suas próprias opções,
        // mas as recebe de fora.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
            : base(opt) // ': base(options)' chama o construtor da classe base (DbContext),
                            // passando as opções para ele. O DbContext base usa essas opções
                            // para se configurar corretamente.
        {
            // O corpo do construtor está vazio porque toda a configuração necessária
            // é feita pela classe base DbContext através das 'options' injetadas.
            // Você poderia adicionar código aqui se precisasse de alguma lógica de inicialização
            // específica para o seu ApplicationDbContext, mas geralmente não é necessário para
            // configurações básicas.
        }
    }
}