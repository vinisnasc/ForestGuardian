using System.ComponentModel.DataAnnotations;

namespace ForestGuardian.Identity.API.Models;

    public class UsuarioResetarSenha
    {
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Token { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string NovaSenha { get; set; }
}