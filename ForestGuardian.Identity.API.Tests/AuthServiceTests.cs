using ForestGuardian.Identity.API.Models;
using ForestGuardian.Identity.API.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ForestGuardian.Identity.API.Tests;

[Collection(nameof(UsuarioCollection))]
public class AuthServiceTests
{
    private readonly UsuarioFixture _usuarioFixture;
    private readonly AuthService _authService;

    public AuthServiceTests(UsuarioFixture usuarioFixture)
    {
        _usuarioFixture = usuarioFixture;
        _authService = _usuarioFixture.AuthService;
    }

    [Fact(DisplayName = "Login com dados corretos deve logar o usuário")]
    public async Task Login_DadosCorretos_DeveLogar()
    {
        // Arrange
        UsuarioLogin usuarioLogin = new UsuarioLogin
        {
            Email = "vini.souza00@gmail.com",
            Senha = "123456Ab!"
        };

        var usuario = _usuarioFixture.GerarUsuarioValido();

        _usuarioFixture.MockUserManager.Setup(x => x.FindByEmailAsync(usuarioLogin.Email))
                                       .ReturnsAsync(usuario);

        _usuarioFixture.MockSignInManager.Setup(x => x.PasswordSignInAsync(usuario, usuarioLogin.Senha, false, true))
                                         .ReturnsAsync(SignInResult.Success);

        _usuarioFixture.MockUserManager.Setup(x => x.GetClaimsAsync(usuario)).ReturnsAsync(new List<System.Security.Claims.Claim>());

        _usuarioFixture.MockUserManager.Setup(x => x.GetRolesAsync(usuario)).ReturnsAsync(new List<string>());

        // act
        var result = await _authService.Login(usuarioLogin);

        // assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result.AccessToken));
        _usuarioFixture.MockUserManager.Verify(x => x.FindByEmailAsync(usuarioLogin.Email), Times.Once);
        _usuarioFixture.MockSignInManager.Verify(x => x.PasswordSignInAsync(usuario, usuarioLogin.Senha, false, true), Times.Once);
    }

    [Fact(DisplayName = "Login com email inexistente deve lançar exceção")]
    public async Task Login_EmailInexistente_DeveLancarExcecao()
    {
        // Arrange
        UsuarioLogin usuarioLogin = new UsuarioLogin
        {
            Email = "vini.souza00@gmail.com",
            Senha = "123456Ab!"
        };

        _usuarioFixture.MockUserManager.Setup(x => x.FindByEmailAsync(usuarioLogin.Email))
                                       .ReturnsAsync((UsuarioAplicacao)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await _authService.Login(usuarioLogin));
        _usuarioFixture.MockSignInManager.Verify(x => x.PasswordSignInAsync(new UsuarioAplicacao(), usuarioLogin.Senha, false, true), Times.Never);
    }

    [Fact(DisplayName = "Login com senha errada deve lançar exceção")]
    public async Task Login_SenhaErrada_DeveLancarExcessao()
    {
        // Arrange
        UsuarioLogin usuarioLogin = new UsuarioLogin
        {
            Email = "vini.souza00@gmail.com",
            Senha = "123456Ab!"
        };

        var usuario = _usuarioFixture.GerarUsuarioValido();

        _usuarioFixture.MockUserManager.Setup(x => x.FindByEmailAsync(usuarioLogin.Email))
                                        .ReturnsAsync(usuario);

        _usuarioFixture.MockSignInManager.Setup(x => x.PasswordSignInAsync(usuario, usuarioLogin.Senha, false, true))
                                        .ReturnsAsync(SignInResult.Failed);

        // assert & act
        await Assert.ThrowsAsync<Exception>(async () => await _authService.Login(usuarioLogin));
    }

    [Fact(DisplayName = "Resetar senha com email inexistente deve lançar exceção")]
    public async Task ResetarSenha_EmailInexistente_DeveLancarExcessao()
    {
        // Arrange
        var model = new UsuarioResetarSenha
        {
            Email = "vini.souza00@gmail.com"
        };

        _usuarioFixture.MockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                                       .ReturnsAsync((UsuarioAplicacao)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await _authService.ResetarSenha(model));
    }

    [Fact]
    public async Task ResetarSenha_DadosCorretos_DeveResetarSenha()
    {
        // Arrange
        var model = new UsuarioResetarSenha
        {
            Email = "vini.souza00@gmail.com",
            Token = "abvcfdsgkfsgk",
            NovaSenha = "novaSenha123!"
        };

        var usuario = _usuarioFixture.GerarUsuarioValido();

        _usuarioFixture.MockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                                       .ReturnsAsync(usuario);

        _usuarioFixture.MockUserManager.Setup(x => x.ResetPasswordAsync(usuario, model.Token, model.NovaSenha))
                                       .ReturnsAsync(IdentityResult.Success);

        await _authService.ResetarSenha(model);

        // Act & Assert
        _usuarioFixture.MockUserManager.Verify(x => x.ResetPasswordAsync(usuario, model.Token, model.NovaSenha), Times.Once);
    }
}