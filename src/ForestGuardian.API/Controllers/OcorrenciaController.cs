using ForestGuardian.Domain.Interfaces.Service;
using ForestGuardian.Domain.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace ForestGuardian.API.Controllers;

public class OcorrenciaController : BaseController
{
    private readonly IOcorrenciaService _ocorrenciaService;

    public OcorrenciaController(INotificator notificator, IOcorrenciaService ocorrenciaService) : base(notificator)
    {
        _ocorrenciaService = ocorrenciaService;
    }

    [HttpGet(Name = "buscarTodos")]
    public async Task<IActionResult> BuscarTodos()
    {
        var ocorrencias = await _ocorrenciaService.BuscarTodos();
        return CustomResponse(ocorrencias);
    }

    [HttpGet("{id}", Name = "buscarPorId")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var ocorrencia = await _ocorrenciaService.BuscarPorId(id);
        return CustomResponse(ocorrencia);
    }

    [HttpPost(Name = "cadastrar")]
    public IActionResult Cadastrar(OcorrenciaInputModel model)
    {
        _ocorrenciaService.Adicionar(model, Guid.NewGuid());
        return CustomResponse();
    }
}