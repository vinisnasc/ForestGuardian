namespace ForestGuardian.API.Extensions;

public class AuthSettingsConfiguration
{
    public string Secret { get; set; }
    public int ExpiracaoHoras { get; set; }
    public string Emissor { get; set; }
    public string ValidoEm { get; set; }
}