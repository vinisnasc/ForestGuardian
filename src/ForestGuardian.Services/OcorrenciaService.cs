using ForestGuardian.Domain.Entities;
using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Domain.Models;

namespace ForestGuardian.Services
{
    public class OcorrenciaService
    {
        private readonly IOcorrenciaRepository _ocorrenciaRepository;

        public OcorrenciaService(IOcorrenciaRepository ocorrenciaRepository)
        {
            _ocorrenciaRepository = ocorrenciaRepository;
        }

        public void Adicionar(OcorrenciaInputModel model, Guid idUsuario)
        {
            var entity = new Ocorrencia(model.Titulo, model.Descricao, model.Latitude, model.Longitude, idUsuario);
            _ocorrenciaRepository.Incluir(entity);
        }
    }
}
