using ForestGuardian.Domain.Models;

namespace ForestGuardian.Domain.Interfaces.Service;

public interface IOcorrenciaService
{
    Task Adicionar(OcorrenciaInputModel model, Guid idUsuario);
}