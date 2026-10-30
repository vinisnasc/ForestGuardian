using ForestGuardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForestGuardian.Infra.Mapping;

public class OcorrenciaMapping : IEntityTypeConfiguration<Ocorrencia>
{
    public void Configure(EntityTypeBuilder<Ocorrencia> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Titulo).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.Descricao).HasColumnType("varchar(500)").IsRequired();
        builder.Property(x => x.DataRegistro).HasColumnType("datetime").IsRequired();
        builder.Property(x => x.StatusOcorrencia).HasColumnType("int").IsRequired();
        builder.Property(x => x.IdUsuarioAberturaOcorrencia).HasColumnType("uniqueidentifier").IsRequired();
        builder.OwnsOne(x => x.Coordenadas, coordenadas =>
        {
            coordenadas.Property(c => c.Latitude).HasColumnName("Latitude").HasColumnType("float").IsRequired();
            coordenadas.Property(c => c.Longitude).HasColumnName("Longitude").HasColumnType("float").IsRequired();
        });

        builder.Navigation(x => x.Coordenadas).IsRequired();
    }
}