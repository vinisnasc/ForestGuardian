using ForestGuardian.Identity.API.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace ForestGuardian.Identity.API.Models
{
    public class UsuarioAplicacao : IdentityUser
    {
        public string? Crea { get; set; }

        public bool CreaValido(Perfil perfil)
        {
            Crea = Crea?.Trim().Replace(".", "").Replace("-", "").Replace(" ", "");

            if (perfil != Perfil.AnalistaAmbiental && string.IsNullOrEmpty(Crea))
                return true;

            else if(perfil != Perfil.AnalistaAmbiental && !string.IsNullOrEmpty(Crea))
                return false;

            if (!long.TryParse(Crea, out long numeroCrea))
                return false;

            if (Crea?.Length >= 9 && Crea?.Length <= 11)
                return true;

            else return false;
        }
    }
}
