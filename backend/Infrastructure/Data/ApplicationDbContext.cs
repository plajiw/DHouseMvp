using Microsoft.EntityFrameworkCore;
using DHouseMvp.Core.Entities;

namespace DHouseMvp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<ServicoOferecido> Servicos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
            : base(opt) { }
    }
}