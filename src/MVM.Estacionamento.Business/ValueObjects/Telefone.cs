using System;
namespace MVM.Estacionamento.Business.ValueObjects;

public record Telefone
{
    public Telefone()
    {
    }

    public string Ddd { get; set; }
    public string TelefoneCelular { get; set; }
    public string TelefoneFixo { get; set; }
}

