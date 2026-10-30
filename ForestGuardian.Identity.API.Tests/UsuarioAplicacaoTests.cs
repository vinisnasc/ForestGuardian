using ForestGuardian.Identity.API.Models;
using ForestGuardian.Identity.API.Models.Enums;

namespace ForestGuardian.Identity.API.Tests;

public class UsuarioAplicacaoTests
{
    [Theory]
    [InlineData(Perfil.Administrador)]
    [InlineData(Perfil.Usuario)]
    public void CreaValido_PerfilNaoAnalistaAmbiental_DeveAceitarCreaNulo(Perfil perfil)
    {
        // Arrange
        var usuario = new UsuarioAplicacao();

        // Act
        var resultado = usuario.CreaValido(perfil);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void CreaValido_PerfilAnalistaAmbiental_NaoDeveAceitarCreaNulo()
    {
        // Arrange
        var usuario = new UsuarioAplicacao();

        // Act
        var resultado = usuario.CreaValido(Perfil.AnalistaAmbiental);

        // Assert
        Assert.False(resultado);
    }

    [Theory]
    [InlineData(Perfil.Administrador)]
    [InlineData(Perfil.Usuario)]
    public void CreaValido_QuandoPerfilDiferenteDeAnalistaAmbiental_NaoDeveAceitarCrea(Perfil perfil)
    {
        // Arrange
        var usuario = new UsuarioAplicacao
        {
            Crea = "123456"
        };

        // Act
        var resultado = usuario.CreaValido(perfil);

        // Assert
        Assert.False(resultado);
    }

    [Theory]
    [InlineData("543254435222")]
    [InlineData("12333333")]
    public void CreaInvalido_QuandoPerfilAnalistaAmbiental_DeveAceitarTamanhoEntreNoveEOnze(string crea)
    {
        // Arrange
        var usuario = new UsuarioAplicacao
        {
            Crea = crea
        };

        // Act
        var resultado = usuario.CreaValido(Perfil.AnalistaAmbiental);

        // Assert
        Assert.False(resultado);
    }

    [Theory]
    [InlineData("543254435")]
    [InlineData("0000000000")]
    [InlineData("54325443533")]
    public void CreaValido_QuandoPerfilAnalistaAmbiental_DeveAceitarTamanhoEntreNoveEOnze(string crea)
    {
        // Arrange
        var usuario = new UsuarioAplicacao
        {
            Crea = crea
        };

        // Act
        var resultado = usuario.CreaValido(Perfil.AnalistaAmbiental);

        // Assert
        Assert.Equal(true, resultado);
    }

    [Fact]
    public void CreaValido_CreaComCaracteresEspeciais_DeveRemoverCaracteresEspeciais()
    {
        // Arrange
        var usuario = new UsuarioAplicacao
        {
            Crea = "1 2.345-6  784 "
        };

        // Act
        var resultado = usuario.CreaValido(Perfil.AnalistaAmbiental);
        
        // Assert
        Assert.True(resultado);
        Assert.Equal("123456784", usuario.Crea);
    }

    [Fact]
    public void CreaValido_ComCaracteresEspeciaisOuLetras_DeveRetornarFalso()
    {
        // Arrange
        var usuario = new UsuarioAplicacao
        {
            Crea = "12.345-678A"
        };

        // Act
        var resultado = usuario.CreaValido(Perfil.AnalistaAmbiental);

        // Assert
        Assert.False(resultado);
    }
}