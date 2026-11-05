using ForestGuardian.Domain.Models.InputModels;
using ForestGuardian.Domain.Models.ViewModels;

namespace ForestGuardian.Domain.Interfaces.Service;

public interface IOcorrenciaService
{
    Task<IEnumerable<OcorrenciaViewModel>> BuscarTodos();
    Task<OcorrenciaViewModel> BuscarPorId(Guid id);
    Task Adicionar(OcorrenciaInputModel model, Guid idUsuario);
}