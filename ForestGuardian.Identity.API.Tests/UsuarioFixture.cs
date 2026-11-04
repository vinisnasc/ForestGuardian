using ForestGuardian.Identity.API.Extensions;
using ForestGuardian.Identity.API.Models;
using ForestGuardian.Identity.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;

namespace ForestGuardian.Identity.API.Tests
{
    [CollectionDefinition(nameof(UsuarioCollection))]
    public class UsuarioCollection : ICollectionFixture<UsuarioFixture>
    {

    }

    public class UsuarioFixture : IDisposable
    {
        public Mock<UserManager<UsuarioAplicacao>> MockUserManager;
        public Mock<SignInManager<UsuarioAplicacao>> MockSignInManager;
        public AuthService AuthService;

        public UsuarioFixture()
        {
            var userStore = new Mock<IUserStore<UsuarioAplicacao>>();

            MockUserManager = new Mock<UserManager<UsuarioAplicacao>>(
            userStore.Object,
            Options.Create(new IdentityOptions()),
            Mock.Of<IPasswordHasher<UsuarioAplicacao>>(),
            Array.Empty<IUserValidator<UsuarioAplicacao>>(),
            Array.Empty<IPasswordValidator<UsuarioAplicacao>>(),
            Mock.Of<ILookupNormalizer>(),
            new IdentityErrorDescriber(),
            null,
            Mock.Of<ILogger<UserManager<UsuarioAplicacao>>>()
        );

            MockSignInManager = new Mock<SignInManager<UsuarioAplicacao>>(
                MockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<UsuarioAplicacao>>(),
                Options.Create(new IdentityOptions()),
                Mock.Of<ILogger<SignInManager<UsuarioAplicacao>>>(),
                Mock.Of<IAuthenticationSchemeProvider>(),
                Mock.Of<IUserConfirmation<UsuarioAplicacao>>()
            );

            var configuration = Options.Create(new IdentityConfiguration
            {
                Secret = "ForestGuardianSecretKey123456789",
                Emissor = "ForestGuardian",
                ValidoEm = "ForestGuardian",
                ExpiracaoHoras = 2
            });

            AuthService = new AuthService(configuration, MockSignInManager.Object, MockUserManager.Object);
        }

        public UsuarioAplicacao GerarUsuarioValido()
        {
            var user = new UsuarioAplicacao
            {
                Id = Guid.NewGuid().ToString(),
                Email = "vini.souza00@gmail.com",
                UserName = "vini.souza00@gmail.com"
            };

            return user;
        }

        public void Dispose() { }
    }
}
