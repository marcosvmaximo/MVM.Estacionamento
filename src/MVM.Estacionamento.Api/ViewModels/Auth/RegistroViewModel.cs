using System;
using System.ComponentModel.DataAnnotations;

namespace MVM.Estacionamento.Api.ViewModels.Auth;

public class RegistroViewModel
{
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O Email não está em um formato válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "A Senha deve ter no mínimo 6 caracteres.")]
    public string Senha { get; set; }

    [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmarSenha { get; set; }
}

