using System;
using AutoMapper;
using MVM.Estacionamento.Api.ViewModels;
using MVM.Estacionamento.Api.ViewModels.Veiculo;
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
        CreateMap<Veiculo, VeiculoViewModel>()
                .ForMember(dest => dest.RegistrosEstacionamento, opt =>
                    opt.MapFrom(src => src.RegistrosEstacionamento.Select(he => new RegistroEstacionamentoViewModel
                    {
                        Id = he.Id,
                        Data = he.Data,
                        HorarioEntrada = he.HorarioEntrada,
                        HorarioSaida = he.HorarioSaida,
                        TempoUtilizado = he.TempoUtilizado
                    }))).ReverseMap();
        CreateMap<RegistroEstacionamento, RegistroEstacionamentoViewModel>().ReverseMap();
    }
}

