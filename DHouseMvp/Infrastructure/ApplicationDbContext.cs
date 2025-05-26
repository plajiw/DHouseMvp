using Microsoft.EntityFrameworkCore;
using DHouseMvp.Core.Entities;    // <- traga aqui Imovel e ServicoOferecido

namespace DHouseMvp.Infrastructure.Data  // <- include o ".Data"
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<ServicoOferecido> Servicos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
