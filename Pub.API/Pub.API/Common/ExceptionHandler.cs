using Pub.API.Model.Response;
using Pub.Core.Common;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

namespace Pub.API.Common {
    /// <summary>
    /// Globális hibakezelő middleware.
    /// </summary>
    public class ExceptionHandler {
        //Tartalmazza a http-kérés eredményét.
        private readonly RequestDelegate requestDelegate;

        //A log service példánya. Szükség lesz rá az esetleges hibák naplózásához.
        //private ILoggerManager _loggerManager;
        public ExceptionHandler(RequestDelegate requestDelegate) {
           this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await requestDelegate(context);

            } //Ha hiba történt a kérés során, akkor belép.
            catch (Exception ex) {
                //Naplózza a hibát.
                LogHandler.LogError(ex.Message);

                //meghívja a hibakezelő függvényt
                await HandleExceptionAsync(context);
            }
        }

        /// <summary>
        /// Megkapja a hibát, létrehozzá a válasz-törzset, majd visszaadja azt.
        /// </summary>
        /// <param name="context">A http-kérés által létrejött kontextus. Tartalmazza az információkat a hibáról.</param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context) {
            //json válasz
            context.Response.ContentType = "application/json";

            //500-as hiba beállítása.
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //egyedi válasz
            ErrorResponse error = new ErrorResponse { ErrorCode = context.Response.StatusCode, Message = Messages.InternalServerError};

            var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();

            var jsonResult = JsonSerializer.Serialize(error, jsonOptions?.Value.SerializerOptions);

            //visszaadja a választ
            await context.Response.WriteAsync(jsonResult);
        }
    }
}
