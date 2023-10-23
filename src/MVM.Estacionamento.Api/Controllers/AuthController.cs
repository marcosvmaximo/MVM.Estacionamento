using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MVM.Estacionamento.Api.Configuration.Auth;
using MVM.Estacionamento.Api.ViewModels.Auth;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
public class AuthController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtModel _jwtConfig;

    public AuthController(INotifyBus notifyBus,
                          IMapper mapper,
                          SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager,
                          IOptions<JwtModel> jwtConfig) : base(notifyBus, mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtConfig = jwtConfig.Value;
    }

    /// <summary>
    /// Registra usuário na aplicação
    /// </summary>
    /// <returns>Bearer Token válido</returns>
    /// 
    /// <remarks>
    /// Cadastra o usuário e o autentica no sistema, retornando o bearer token
    /// <br></br>
    /// <br></br>
    /// Objeto padrão das respostas: <br></br>
    ///     "httpCode" : 200,<br></br>
    ///     "sucess" : true,<br></br>
    ///     "message" "Requisição enviada com sucesso.",<br></br>
    ///     "result": {}<br></br>
    /// </remarks>
    /// <response code="200">Sucesso: Retorna um bearer token válido</response>
    /// <response code="400">Falha: Se ocorreu algum problema ao registrar o usuário</response>
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

        return await CustomResponse(await GerarToken(model.Email));
    }

    /// <summary>
    /// Faz login do usuário
    /// </summary>
    /// <returns>Bearer Token válido</returns>
    /// 
    /// <remarks>
    /// Realiza a busca das credenciais inseridas e retorna o token se validado.
    /// <br></br>
    /// <br></br>
    /// Objeto padrão das respostas: <br></br>
    ///     "httpCode" : 200,<br></br>
    ///     "sucess" : true,<br></br>
    ///     "message" "Requisição enviada com sucesso.",<br></br>
    ///     "result": {}<br></br>
    /// </remarks>
    /// <response code="200">Sucesso: Retorna um bearer token válido</response>
    /// <response code="400">Falha: Se ocorreu algum problema ao registrar o usuário</response>
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);
        if (!result.Succeeded)
        {
            await Notify("Usuário ou senha incorreto");
            return await CustomResponse();
        }
        if (result.IsLockedOut)
        {
            await Notify("Usuário temporariamente bloqueado devido ao número de falhas");
            return await CustomResponse();
        }

        return await CustomResponse(await GerarToken(model.Email));
    }

    /// <summary>
    /// Gera token apartir da credencial user/email
    /// </summary>
    /// <returns>Gera o bearer token</returns>
    /// 
    private async Task<object> GerarToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Issuer = _jwtConfig.Emissor,
            Audience = _jwtConfig.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_jwtConfig.TempoExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        });

        var encondedToken = tokenHandler.WriteToken(token);
        var response = new
        {
            AcessToken = encondedToken,
            ExpiresIn = TimeSpan.FromHours(_jwtConfig.TempoExpiracaoHoras).TotalSeconds,
            User = new
            {
                Id = user.Id!,
                Email = user.Email!,
                Claims = claims.Select(x => new { Type = x.Type, Value = x.Value })
            }
        };

        return response;
    }

    private static long ToUnixEpochDate(DateTime date)
    {
        return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}

