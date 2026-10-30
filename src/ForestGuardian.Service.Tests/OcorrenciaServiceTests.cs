using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Domain.Models;
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
        var ocorrenciaRepository = new Mock<IOcorrenciaRepository>();
        var ocorrenciaService = new OcorrenciaService(ocorrenciaRepository.Object);
        var model = new OcorrenciaInputModel(ocorrencia.Titulo, ocorrencia.Descricao, ocorrencia.Coordenadas.Latitude, ocorrencia.Coordenadas.Longitude);
        
        // Act
        ocorrenciaService.Adicionar(model, ocorrencia.IdUsuarioAberturaOcorrencia);

        // Assert
        ocorrenciaRepository.Verify(x => x.Incluir(It.IsAny<ForestGuardian.Domain.Entities.Ocorrencia>()), Times.Once);
    }
}