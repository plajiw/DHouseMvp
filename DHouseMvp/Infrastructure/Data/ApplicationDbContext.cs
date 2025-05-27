using Microsoft.EntityFrameworkCore;
using DHouseMvp.Core.Entities;

namespace DHouseMvp.Infrastructure.Data
{
    // 'public class ApplicationDbContext : DbContext'
    // Declara uma classe p�blica chamada ApplicationDbContext.
    // O ':' indica que ApplicationDbContext "herda de" DbContext.
    // DbContext � uma classe fundamental do Entity Framework Core. Ela representa uma sess�o
    // com o banco de dados e permite que voc� consulte e salve inst�ncias de suas entidades.
    // ApplicationDbContext � a sua "ponte" personalizada para o banco de dados.
    public class ApplicationDbContext : DbContext
    {
        // PROPRIEDADES DbSet<T>:
        // Um DbSet<TEntity> representa uma cole��o de todas as entidades de um determinado tipo
        // no contexto ou que podem ser consultadas a partir do banco de dados.
        // Essencialmente, cada DbSet<T> mapeia para uma tabela no seu banco de dados.

        // 'public DbSet<Imovel> Imoveis { get; set; }'
        // Declara uma propriedade chamada Imoveis do tipo DbSet<Imovel>.
        // Isso informa ao Entity Framework Core que voc� tem uma entidade chamada Imovel
        // (definida em Core/Entities/Imovel.cs) e que voc� quer poder consult�-la e salv�-la.
        // O EF Core, por conven��o, criar� uma tabela chamada "Imoveis" no banco de dados.
        public DbSet<Imovel> Imoveis { get; set; }

        // 'public DbSet<ServicoOferecido> Servicos { get; set; }'
        // Similarmente, isso declara uma cole��o para a sua entidade ServicoOferecido,
        // que ser� mapeada para uma tabela "Servicos".
        public DbSet<ServicoOferecido> Servicos { get; set; }

        // CONSTRUTOR:
        // 'public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)'
        // Este � o construtor da classe ApplicationDbContext.
        // Ele aceita um par�metro 'options' do tipo DbContextOptions<ApplicationDbContext>.
        // Estas 'options' s�o configuradas no arquivo Program.cs (ou Startup.cs em vers�es mais antigas do .NET)
        // e cont�m informa��es importantes como qual provedor de banco de dados usar (SQLite, PostgreSQL, SQL Server, etc.)
        // e a string de conex�o para o banco de dados.
        // Este � um exemplo de Inje��o de Depend�ncia: o ApplicationDbContext n�o cria suas pr�prias op��es,
        // mas as recebe de fora.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
            : base(opt) // ': base(options)' chama o construtor da classe base (DbContext),
                            // passando as op��es para ele. O DbContext base usa essas op��es
                            // para se configurar corretamente.
        {
            // O corpo do construtor est� vazio porque toda a configura��o necess�ria
            // � feita pela classe base DbContext atrav�s das 'options' injetadas.
            // Voc� poderia adicionar c�digo aqui se precisasse de alguma l�gica de inicializa��o
            // espec�fica para o seu ApplicationDbContext, mas geralmente n�o � necess�rio para
            // configura��es b�sicas.
        }
    }
}