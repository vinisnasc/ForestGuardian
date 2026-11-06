using ForestGuardian.Domain.Entities;
using ForestGuardian.Domain.Interfaces.Infra;
using ForestGuardian.Domain.Interfaces.Service;
using ForestGuardian.Domain.Models.InputModels;
using ForestGuardian.Domain.Models.ViewModels;
using System.Net;

namespace ForestGuardian.Services
{
    public class OcorrenciaService : BaseService, IOcorrenciaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public OcorrenciaService(INotificator notificator, IUnitOfWork unitOfWork, IUserService userService) : base(notificator)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<IEnumerable<OcorrenciaViewModel>> BuscarTodos()
        {
            var ocorrencias = await _unitOfWork.OcorrenciaRepository.SelecionarTudo();
            return ocorrencias.Select(x => new OcorrenciaViewModel(x.Id, x.Titulo, x.Descricao, x.Coordenadas, x.StatusOcorrencia));
        }

        public async Task<OcorrenciaViewModel> BuscarPorId(Guid id)
        {
            var ocorrencia = await _unitOfWork.OcorrenciaRepository.SelecionarPorId(id);

            if (ocorrencia is null) 
            {
                Notificate("Não foi encontrado ocorrencia com o id informado!", HttpStatusCode.NotFound);
                return null;
            }

            return new OcorrenciaViewModel(ocorrencia.Id, ocorrencia.Titulo, ocorrencia.Descricao, ocorrencia.Coordenadas, ocorrencia.StatusOcorrencia);
        }

        public async Task Adicionar(OcorrenciaInputModel model, Guid idUsuario)
        {
            var entity = new Ocorrencia(model.Titulo, model.Descricao, model.Latitude, model.Longitude, _userService.GetUserId());
            await _unitOfWork.OcorrenciaRepository.Incluir(entity);
        }

    }
}
