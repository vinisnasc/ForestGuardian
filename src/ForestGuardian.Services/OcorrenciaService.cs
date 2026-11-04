using ForestGuardian.Domain.Entities;
using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Domain.Interfaces.Service;
using ForestGuardian.Domain.Models;

namespace ForestGuardian.Services
{
    public class OcorrenciaService : IOcorrenciaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OcorrenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Adicionar(OcorrenciaInputModel model, Guid idUsuario)
        {
            var entity = new Ocorrencia(model.Titulo, model.Descricao, model.Latitude, model.Longitude, idUsuario);
            _unitOfWork.OcorrenciaRepository.Incluir(entity);
        }
    }
}
