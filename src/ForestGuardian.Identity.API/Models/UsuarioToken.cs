namespace ForestGuardian.Identity.API.Models;

public class UsuarioToken
{
    public string Id { get; set; }
    public string Identificacao { get; set; }
    public IEnumerable<UsuarioClaim> Claims { get; set; }
}