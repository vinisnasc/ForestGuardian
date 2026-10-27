using ForestGuardian.Identity.API.Models.Enums;
using ForestGuardian.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForestGuardian.Identity.API.Data;

public class Context : IdentityDbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<UsuarioAplicacao> UsuariosAplicacao { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1d7c3e7b-2fbc-4a1b-8a45-1e2eae6f9d8a", Name = Perfil.Administrador.ToString(), NormalizedName = Perfil.Administrador.ToString().ToString() },
            new IdentityRole { Id = "2e8b9d3c-3fac-4c2b-9d56-2f4c4e7e8d9b", Name = Perfil.AnalistaAmbiental.ToString(), NormalizedName = Perfil.AnalistaAmbiental.ToString().ToUpper() },
            new IdentityRole { Id = "2e8b9d3c-3fac-4c2b-9d56-2f3c4e7e8d9b", Name = Perfil.Usuario.ToString(), NormalizedName = Perfil.Usuario.ToString().ToUpper() }
        );
    }
}