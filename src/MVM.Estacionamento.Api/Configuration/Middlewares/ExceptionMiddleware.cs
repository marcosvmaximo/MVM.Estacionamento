using System;
using System.Net;
using Elmah.Io.AspNetCore;

namespace MVM.Estacionamento.Api.Configuration.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        ex.Ship(context);
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
}

