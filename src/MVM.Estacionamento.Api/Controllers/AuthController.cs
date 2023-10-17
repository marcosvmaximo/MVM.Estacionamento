using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVM.Estacionamento.Api.ViewModels.Auth;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Api.Controllers;

[Route("api")]
public class AuthController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(INotifyBus notifyBus,
                          IMapper mapper,
                          SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager) : base(notifyBus, mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar([FromBody] RegistroViewModel model)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        IdentityUser user = new IdentityUser()
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Senha);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                await Notify(error.Description);
            }
            return await CustomResponse(model);
        }

        await _signInManager.SignInAsync(user, false);

        return await CustomResponse(model);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);
        if (!result.Succeeded)
            await Notify("Usuário ou senha incorreto");
        if (result.IsLockedOut)
            await Notify("Usuário temporariamente bloqueado devido ao número de falhas");

        return await CustomResponse(model);
    }
}

