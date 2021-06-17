namespace Tcp.TechChallenge.Infra.Context
{
    using Microsoft.EntityFrameworkCore;
    using Tcp.TechChallenge.Infra.Mapping;
    using Tcp.TechChallenge.Infra.Models;

    public class ConteinerContext : DbContext
    {
        public DbSet<Conteiner> Conteiners => Set<Conteiner>();
        public ConteinerContext(DbContextOptions<ConteinerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConteinerMap());
        }
    }
}
