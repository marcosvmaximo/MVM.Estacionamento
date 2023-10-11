using System;
using AutoMapper;
using MVM.Estacionamento.Api.ViewModels;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Business.ValueObjects;

namespace MVM.Estacionamento.Api.Configuration;

public class EmpresaProfile : Profile
{
    public EmpresaProfile()
    {
        CreateMap<Empresa, EmpresaViewModel>().ReverseMap();
        CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        CreateMap<Telefone, TelefoneViewModel>().ReverseMap();
    }
}

