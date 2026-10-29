using ForestGuardian.Domain.Entities;
using ForestGuardian.Domain.Enums;

namespace ForestGuardian.Domain.Tests;

public class OcorrenciaTests
{
    [Fact]
    public void Ocorrencia_TituloAcimaDoPermitido_NaoDeveCriarObjeto()
    {
        Assert.Throws<ArgumentException>(() => new Ocorrencia("Um titulo com mais de 100 caracteres que excede o limite permitido Um titulo com mais de 100 caracteres que excede o limite permitido"
                                      , "Descricao valida", 90, 90, Guid.NewGuid()));
    }

    [Fact]
    public void Ocorrencia_DescricaoAcimaDoPermitido_NaoDeveCriarObjeto()
    {
        Assert.Throws<ArgumentException>(() => new Ocorrencia("Titulo valido"
                                      , "Descricao invalida com mais de 500 caracteres que excede o limite permitido Descricao invalida com mais de 500 caracteres que excede o limite permitido"
                                      + "Descricao invalida com mais de 500 caracteres que excede o limite permitido Descricao invalida com mais de 500 caracteres que excede o limite permitido"
                                      + "Descricao invalida com mais de 500 caracteres que excede o limite permitido Descricao invalida com mais de 500 caracteres que excede o limite permitido"
                                      + "Descricao invalida com mais de 500 caracteres que excede o limite permitido Descricao invalida com mais de 500 caracteres que excede o limite permitido"
                                      , 90, 90, Guid.NewGuid()));
    }

    [Fact]
    public void Ocorrencia_ValoresValidos_DeveCriarObjeto()
    {
        // Arrange 
        var ocorrencia = new Ocorrencia("Titulo valido", "Descricao valida", 90, 90 ,Guid.NewGuid());

        // Act
        var result = ocorrencia.EhValido();

        // Assert
        Assert.True(result);
        Assert.Equal(0, ocorrencia.ValidationResult.Errors.Count);
        Assert.IsType<Ocorrencia>(ocorrencia);
    }

    [Fact]
    public void Ocorrencia_ValoresValidos_StatusDeveSerCriadaComoAberta()
    {
        // Arrange & Act
        var ocorrencia = new Ocorrencia("Titulo valido", "Descricao valida", 90, 90, Guid.NewGuid());

        // Assert
        Assert.True(ocorrencia.EhValido());
        Assert.Equal(StatusOcorrencia.Aberta, ocorrencia.StatusOcorrencia);
    }

    [Fact]
    public void Ocorrencia_ValoresInvalidos_NaoDeveCriarObjeto()
    {
        // Arrange 
        Assert.Throws<ArgumentException>(() => new Ocorrencia("    ", "Descricao valida", 90, 90, Guid.NewGuid()));
    }

    [Fact]
    public void Ocorrencia_UsuarioInvalido_NaoDeveCriarObjeto()
    {
        // Arrange 
        Assert.Throws<ArgumentException>(() => new Ocorrencia("Titulo valido", "Descricao valida", 90, 90, Guid.Empty));
    }

    [Fact]
    public void Ocorrencia_AlterarStatus_DeveAtualizarStatusOcorrencia()
    {
        // Arrange
        var ocorrencia = new Ocorrencia("Titulo valido", "Descricao valida", 90, 90, Guid.NewGuid());

        // Act
        ocorrencia.AtualizarStatus(Enums.StatusOcorrencia.Resolvida);

        // Assert
        Assert.Equal(Enums.StatusOcorrencia.Resolvida, ocorrencia.StatusOcorrencia);
    }

    [Fact]
    public void AlterarStatus_ComDadosInvalidos_DeveGerarExcecao()
    {
        // Arrange
        var ocorrencia = new Ocorrencia("Titulo valido", "Descricao valida", 90, 90, Guid.NewGuid());

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => ocorrencia.AtualizarStatus((StatusOcorrencia)55));
    }
}