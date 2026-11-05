using ForestGuardian.Domain.Enums;
using ForestGuardian.Domain.ValueObjects;

namespace ForestGuardian.Domain.Models.ViewModels;

public record OcorrenciaViewModel(Guid Id,string Titulo, string Descricao, Coordenadas Coordenadas, StatusOcorrencia StatusOcorrencia)
{
}