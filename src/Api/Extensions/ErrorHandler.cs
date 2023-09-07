using System.Net;
using Application.DTO.Response;
using Domain.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Extensions;

public static class ErrorHandler
{
    public static void StartErrorHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(
            errorApp =>
            {
                errorApp.Run(async ctx =>
                {
                    var exptHandlerPathFeat = ctx.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exptHandlerPathFeat?.Error;

                    var code = (int)HttpStatusCode.InternalServerError;
                    var message = exception?.Message;

                    if (exception is BadArgumentException)
                    {
                        code = (int)HttpStatusCode.BadRequest;
                        message = exception.Message;
                    }

                    ctx.Response.StatusCode = code;
                    ctx.Response.ContentType = "application/json";
                    await ctx.Response.WriteAsJsonAsync(new ErrorDto(
                        code, message
                    ));
                });
            }
            );
    }
}