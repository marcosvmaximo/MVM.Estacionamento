using System;
namespace MVM.Estacionamento.Api.Controllers.Common;

public sealed class BaseResponse
{
    public int HttpCode { get; set; }
    public bool Sucess { get; set; }
    public string? Message { get; set; }
    public object? Result { get; set; }
}

