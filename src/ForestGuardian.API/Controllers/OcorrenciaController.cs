using ForestGuardian.Domain.Interfaces.Service;
using ForestGuardian.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForestGuardian.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcorrenciaController : ControllerBase
    {
        private readonly IOcorrenciaService _ocorrenciaService;

        public OcorrenciaController( IOcorrenciaService ocorrenciaService)
        {
            _ocorrenciaService = ocorrenciaService;
        }

        [HttpPost(Name = "cadastrar")]
        public IActionResult Cadastrar(OcorrenciaInputModel model)
        {
            _ocorrenciaService.Adicionar(model, Guid.NewGuid());
            return Ok();
        }
    }
}
