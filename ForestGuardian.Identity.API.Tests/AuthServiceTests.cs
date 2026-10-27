using ForestGuardian.Identity.API.Extensions;
using ForestGuardian.Identity.API.Models;
using ForestGuardian.Identity.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ForestGuardian.Identity.API.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task Login_EmailNaoCadastrado_DeveLancarExcecao()
    {
        // Arrange
        var userManager = CriarUserManagerMock();
        var signInManager = CriarSignInManagerMock(userManager);

        var configuracao = Options.Create(new IdentityConfiguration
        {
            Secret = "uma-chave-de-teste-com-tamanho-suficiente-123456",
            Emissor = "ForestGuardian",
            ValidoEm = "ForestGuardian.Tests",
            ExpiracaoHoras = 1
        });

        var service = new AuthService(configuracao, signInManager.Object, userManager.Object);

        var model = new UsuarioLogin
        {
            Email = "inexistente@teste.com",
            Senha = "Senha123!"
        };

        userManager.Setup(x => x.FindByEmailAsync(model.Email)).ReturnsAsync((UsuarioAplicacao?)null);

        // Act
        var excecao = await Assert.ThrowsAsync<Exception>(() => service.Login(model));

        // Assert
        Assert.Equal("E-mail ou senha inválidos", excecao.Message);

        signInManager.Verify(
            x => x.PasswordSignInAsync(
                It.IsAny<UsuarioAplicacao>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()),
            Times.Never);
    }

    private static Mock<UserManager<UsuarioAplicacao>>CriarUserManagerMock()
    {
        var store = new Mock<IUserStore<UsuarioAplicacao>>();

        var options = Options.Create(new IdentityOptions());

        var passwordHasher =
            new Mock<IPasswordHasher<UsuarioAplicacao>>();

        var userValidators =
            Array.Empty<IUserValidator<UsuarioAplicacao>>();

        var passwordValidators =
            Array.Empty<IPasswordValidator<UsuarioAplicacao>>();

        var keyNormalizer = new Mock<ILookupNormalizer>();
        var errors = new IdentityErrorDescriber();
        var services = new Mock<IServiceProvider>();

        var logger =
            new Mock<ILogger<UserManager<UsuarioAplicacao>>>();

        return new Mock<UserManager<UsuarioAplicacao>>(
            store.Object,
            options,
            passwordHasher.Object,
            userValidators,
            passwordValidators,
            keyNormalizer.Object,
            errors,
            services.Object,
            logger.Object);
    }

    private static Mock<SignInManager<UsuarioAplicacao>> CriarSignInManagerMock(Mock<UserManager<UsuarioAplicacao>> userManager)
    {
        var contextAccessor = new Mock<IHttpContextAccessor>();

        var claimsFactory =
            new Mock<IUserClaimsPrincipalFactory<UsuarioAplicacao>>();

        var options = Options.Create(new IdentityOptions());

        var logger =
            new Mock<ILogger<SignInManager<UsuarioAplicacao>>>();

        var schemes =
            new Mock<IAuthenticationSchemeProvider>();

        var confirmation =
            new Mock<IUserConfirmation<UsuarioAplicacao>>();

        return new Mock<SignInManager<UsuarioAplicacao>>(
            userManager.Object,
            contextAccessor.Object,
            claimsFactory.Object,
            options,
            logger.Object,
            schemes.Object,
            confirmation.Object);
    }
}