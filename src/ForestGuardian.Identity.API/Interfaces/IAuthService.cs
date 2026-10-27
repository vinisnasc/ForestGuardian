using ForestGuardian.Identity.API.Models;

namespace ForestGuardian.Identity.API.Interfaces;

public interface IAuthService
{
    Task<UsuarioRespostaLogin> Registrar(UsuarioRegistro model);
    Task<UsuarioRespostaLogin> Login(UsuarioLogin model);
    Task RecuperarSenha(string email);
    Task ResetarSenha(UsuarioResetarSenha model);
}