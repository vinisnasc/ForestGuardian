namespace ForestGuardian.Identity.API.Extensions;

public class IdentityConfiguration
{
    public string Secret { get; set; }
    public int ExpiracaoHoras { get; set; }
    public string Emissor { get; set; }
    public string ValidoEm { get; set; }
}