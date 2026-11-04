using ForestGuardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ForestGuardian.Infra.Context;

public class ForestGuardianContext : DbContext
{
    public DbSet<Ocorrencia> Ocorrencias { get; set; }

    public ForestGuardianContext(DbContextOptions<ForestGuardianContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                    .Where(x => typeof(BaseEntity).IsAssignableFrom(x.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType)
                .Ignore(nameof(BaseEntity.ValidationResult));
        }

        base.OnModelCreating(modelBuilder);
    }
}