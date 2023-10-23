using System;
namespace MVM.Estacionamento.Api.Configuration.Auth;

public class JwtModel
{
    public string Secret { get; set; }
    public int TempoExpiracaoHoras { get; set; }
    public string Emissor { get; set; }
    public string ValidoEm { get; set; }
}

