using ForestGuardian.Identity.API.Extensions;
using ForestGuardian.Identity.API.Interfaces;
using ForestGuardian.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForestGuardian.Identity.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<UsuarioAplicacao> _userManager;
    private readonly SignInManager<UsuarioAplicacao> _signInManager;
    private readonly IdentityConfiguration _appSettings;

    public AuthService(IOptions<IdentityConfiguration> appSettings, SignInManager<UsuarioAplicacao> signInManager, UserManager<UsuarioAplicacao> userManager)
    {
        _appSettings = appSettings.Value;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<UsuarioRespostaLogin> Registrar(UsuarioRegistro model)
    {
        var user = new UsuarioAplicacao
        {
            UserName = model.Email,
            EmailConfirmed = true,
            Email = model.Email,
            Crea = model.Crea
        };

        if (!user.CreaValido(model.Perfil)) throw new Exception("Crea inválido para esse usuário");

        var userExiste = await _userManager.FindByEmailAsync(user.Email);
        if (userExiste is not null) throw new Exception("Usuario já existe");

        var result = await _userManager.CreateAsync(user, model.Senha);

        if (!result.Succeeded) throw new Exception("Algo errado ocorreu!");

        await _userManager.AddToRoleAsync(user, model.Perfil.ToString());
        await _signInManager.SignInAsync(user, isPersistent: false);
        return await GerarJwt(user);
    }

    public async Task<UsuarioRespostaLogin> Login(UsuarioLogin model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) throw new Exception("E-mail ou senha inválidos");

        var result = await _signInManager.PasswordSignInAsync(user, model.Senha, false, true);

        if (result.Succeeded) return await GerarJwt(user);

        throw new Exception("Algo errado ocorreu!");
    }

    public async Task RecuperarSenha(string email)
    {
        if (string.IsNullOrEmpty(email)) throw new Exception("Informe um e-mail válido!");

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) throw new Exception("não há um usuário vinculado a este email!");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        if (token == null) throw new Exception("Algo errado ocorreu!");

        // todo: enviar email
    }

    public async Task ResetarSenha(UsuarioResetarSenha model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null) throw new Exception("não há um usuário vinculado a este email!");

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NovaSenha);

        if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault().Description);
    }

    private async Task<UsuarioRespostaLogin> GerarJwt(UsuarioAplicacao usuario)
    {
        var claims = await _userManager.GetClaimsAsync(usuario);
        var userRoles = await _userManager.GetRolesAsync(usuario);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach (var role in userRoles)
            claims.Add(new Claim("role", role));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        var response = new UsuarioRespostaLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
            UsuarioToken = new UsuarioToken
            {
                Id = usuario.Id,
                Claims = claims.Select(x => new UsuarioClaim { Type = x.Type, Value = x.Value })
            }
        };

        return response;
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}