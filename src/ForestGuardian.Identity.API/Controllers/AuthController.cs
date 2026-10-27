using ForestGuardian.Identity.API.Interfaces;
using ForestGuardian.Identity.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForestGuardian.Identity.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Registrar(UsuarioRegistro model)
    {
        try
        {
            return Ok(await _authService.Registrar(model));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("logar")]
    public async Task<ActionResult> Login(UsuarioLogin model)
    {
        try
        {
            return Ok(await _authService.Login(model));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("recuperar-senha")]
    public async Task<ActionResult> RecupearSenha(string email)
    {
        try
        {
            await _authService.RecuperarSenha(email);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("resetar-senha")]
    public async Task<ActionResult> ResetarSenha(UsuarioResetarSenha model)
    {
        try
        {
            await _authService.ResetarSenha(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}