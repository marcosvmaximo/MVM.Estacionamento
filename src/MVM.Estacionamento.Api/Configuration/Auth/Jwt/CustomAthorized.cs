using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVM.Estacionamento.Api.Configuration.Auth;

public static class CustomAuthorization
{
    public static bool ValidarClaimsUsuario(HttpContext contexto, string nomeClaim, string valorClaim)
    {
        return contexto.User.Identity!.IsAuthenticated &&
            contexto.User.Claims.Any(x => x.Type == nomeClaim && x.Value.Contains(valorClaim));
    }
}

public class ClaimsAuthorizedAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizedAttribute(string nomeClaim, string valorClaim) : base(typeof(RequisitoClaimFilter))
    {
        Arguments = new object[] { new Claim(nomeClaim, valorClaim) };
    }
}

public class RequisitoClaimFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public RequisitoClaimFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}