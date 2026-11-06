using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Domain.Interfaces.Service;
using ForestGuardian.Domain.Models;
using ForestGuardian.Domain.Models.InputModels;
using ForestGuardian.Domain.Tests;
using ForestGuardian.Services;
using Moq;

namespace ForestGuardian.Service.Tests;

[CollectionDefinition(nameof(OcorrenciaCollection))]
public class OcorrenciaCollection : ICollectionFixture<OcorrenciaFixture> { }

[Collection(nameof(OcorrenciaCollection))]
public class OcorrenciaServiceTests
{
    private readonly OcorrenciaFixture _ocorrenciaFixture;

    public OcorrenciaServiceTests(OcorrenciaFixture ocorrenciaFixture)
    {
        _ocorrenciaFixture = ocorrenciaFixture;
    }

    [Fact(DisplayName = "Adiciona ocorrencia com sucesso")]
    public async Task OcorrenciaService_Adicionar_OcorrenciaComSucesso()
    {
        // Arrange
        var ocorrencia = _ocorrenciaFixture.GerarOcorrenciaValida();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var notificadorMock = new Mock<INotificator>();
        var usuarioServiceMock = new Mock<IUserService>();
        usuarioServiceMock.Setup(x => x.GetUserId()).Returns(Guid.NewGuid());
        unitOfWorkMock.Setup(x => x.OcorrenciaRepository.Incluir(It.IsAny<ForestGuardian.Domain.Entities.Ocorrencia>()));
        var ocorrenciaService = new OcorrenciaService(notificadorMock.Object ,unitOfWorkMock.Object, usuarioServiceMock.Object);
        var model = new OcorrenciaInputModel(ocorrencia.Titulo, ocorrencia.Descricao, ocorrencia.Coordenadas.Latitude, ocorrencia.Coordenadas.Longitude);
        
        // Act
        await ocorrenciaService.Adicionar(model, ocorrencia.IdUsuarioAberturaOcorrencia);

        // Assert
        unitOfWorkMock.Verify(x => x.OcorrenciaRepository.Incluir(It.IsAny<ForestGuardian.Domain.Entities.Ocorrencia>()), Times.Once);
    }

}