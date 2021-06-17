namespace Tcp.TechChallenge.Infra.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Tcp.TechChallenge.Infra.Models;

    public class ConteinerMap : IEntityTypeConfiguration<Conteiner>
    {
        public void Configure(EntityTypeBuilder<Conteiner> builder)
        {
            builder.ToTable("Conteiner");
            builder.HasKey(x => x.Numero).HasName("PK_Numero_Conteiner");
            builder.Property(x => x.Numero).HasColumnName("Numero");
            builder.Property(x => x.TipoOperacao).HasColumnName("Tipo");
            builder.Property(x => x.Capacidade).HasColumnName("Capacidade");
        }
    }
}
