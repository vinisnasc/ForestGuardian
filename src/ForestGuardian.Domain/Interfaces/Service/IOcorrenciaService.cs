using ForestGuardian.Domain.Models;

namespace ForestGuardian.Domain.Interfaces.Service;

public interface IOcorrenciaService
{
    void Adicionar(OcorrenciaInputModel model, Guid idUsuario);
}