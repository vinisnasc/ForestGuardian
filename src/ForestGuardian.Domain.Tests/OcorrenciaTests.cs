using Bogus;
using ForestGuardian.Domain.Entities;
using ForestGuardian.Domain.Enums;

namespace ForestGuardian.Domain.Tests;

[Collection(nameof(OcorrenciaCollection))]
public class OcorrenciaTests
{
    private readonly OcorrenciaFixture _ocorrenciaFixture;

    public OcorrenciaTests(OcorrenciaFixture ocorrenciaFixture) 
    {
        _ocorrenciaFixture = ocorrenciaFixture;
    }

    [Fact(DisplayName = "Ocorrencia com título acima do permitido não deve criar objeto")]
    public void Ocorrencia_TituloAcimaDoPermitido_NaoDeveCriarObjeto()
    {
        Assert.Throws<ArgumentException>(() => new Ocorrencia(new Faker().Lorem.Letter(101)
                                                            , _ocorrenciaFixture.GerarDescricaoValida()
                                                            , _ocorrenciaFixture.GerarLatitudeValida()
                                                            , _ocorrenciaFixture.GerarLongitudeValida()
                                                            , Guid.NewGuid()));
    }

    [Fact(DisplayName = "Ocorrencia com descrição acima do permitido não deve criar objeto")]
    public void Ocorrencia_DescricaoAcimaDoPermitido_NaoDeveCriarObjeto()
    {
        Assert.Throws<ArgumentException>(() => new Ocorrencia(
                                        _ocorrenciaFixture.GerarTituloValido()
                                      , new Faker().Lorem.Letter(501)
                                      , _ocorrenciaFixture.GerarLatitudeValida()
                                      , _ocorrenciaFixture.GerarLongitudeValida()
                                      , Guid.NewGuid()));
    }

    [Fact(DisplayName = "Ocorrencia com valores válidos deve criar objeto")]
    public void Ocorrencia_ValoresValidos_DeveCriarObjeto()
    {
        // Arrange 
        var ocorrencia = _ocorrenciaFixture.GerarOcorrenciaValida();

        // Act
        var result = ocorrencia.EhValido();

        // Assert
        Assert.True(result);
        Assert.Empty(ocorrencia.ValidationResult.Errors);
        Assert.IsType<Ocorrencia>(ocorrencia);
    }

    [Fact(DisplayName = "Ocorrencia com valores válidos deve ter status criada como aberta")]
    public void Ocorrencia_ValoresValidos_StatusDeveSerCriadaComoAberta()
    {
        // Arrange & Act
        var ocorrencia = _ocorrenciaFixture.GerarOcorrenciaValida();

        // Assert
        Assert.True(ocorrencia.EhValido());
        Assert.Equal(StatusOcorrencia.Aberta, ocorrencia.StatusOcorrencia);
    }

    [Theory(DisplayName = "Ocorrencia com título nulo ou vazio não deve criar objeto")]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData(null)]
    public void Ocorrencia_TitulosNulosOuVazios_NaoDeveCriarObjeto(string titulo)
    {
        // Arrange 
        Assert.Throws<ArgumentException>(() => new Ocorrencia(titulo
                                                            , _ocorrenciaFixture.GerarDescricaoValida()
                                                            , _ocorrenciaFixture.GerarLatitudeValida()
                                                            , _ocorrenciaFixture.GerarLongitudeValida()
                                                            , Guid.NewGuid()));
    }

    [Theory(DisplayName = "Ocorrencia com descrição nula ou vazia não deve criar objeto")]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData(null)]
    public void Ocorrencia_DescricoesNulasOuVazias_NaoDeveCriarObjeto(string descricao)
    {
        // Arrange 
        Assert.Throws<ArgumentException>(() => new Ocorrencia(_ocorrenciaFixture.GerarTituloValido()
                                                            , descricao
                                                            , _ocorrenciaFixture.GerarLatitudeValida()
                                                            , _ocorrenciaFixture.GerarLongitudeValida()
                                                            , Guid.NewGuid()));
    }

    [Fact(DisplayName = "Ocorrencia com usuário inválido não deve criar objeto")]
    public void Ocorrencia_UsuarioInvalido_NaoDeveCriarObjeto()
    {
        // Arrange 
        Assert.Throws<ArgumentException>(() => new Ocorrencia(_ocorrenciaFixture.GerarTituloValido()
                                                            , _ocorrenciaFixture.GerarDescricaoValida()
                                                            , _ocorrenciaFixture.GerarLatitudeValida()
                                                            , _ocorrenciaFixture.GerarLongitudeValida()
                                                            , Guid.Empty));
    }

    [Fact(DisplayName = "Ocorrencia com dados válidos para alteração de status deve atualizar o status")]
    public void Ocorrencia_AlterarStatus_DeveAtualizarStatusOcorrencia()
    {
        // Arrange
        var ocorrencia = _ocorrenciaFixture.GerarOcorrenciaValida();

        // Act
        ocorrencia.AtualizarStatus(Enums.StatusOcorrencia.Resolvida);

        // Assert
        Assert.Equal(Enums.StatusOcorrencia.Resolvida, ocorrencia.StatusOcorrencia);
    }

    [Fact(DisplayName = "Ocorrencia com dados inválidos para alteração de status não deve criar objeto")]
    public void AlterarStatus_ComDadosInvalidos_DeveGerarExcecao()
    {
        // Arrange
        var ocorrencia = _ocorrenciaFixture.GerarOcorrenciaValida();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => ocorrencia.AtualizarStatus((StatusOcorrencia)55));
    }
}