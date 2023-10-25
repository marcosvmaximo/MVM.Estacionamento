﻿using System;
namespace MVM.Estacionamento.Business.ValueObjects;

public record Endereco
{
    public Endereco()
    {
    }

    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public int Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
}

