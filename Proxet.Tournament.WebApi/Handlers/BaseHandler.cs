using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Proxet.Tournament.WebApi.Handlers
{
    public abstract class BaseHandler
    {
        protected IActionResult Result(HttpStatusCode code)
        {
            return new StatusCodeResult((int)code);
        }

        protected IActionResult JsonResult<T>(T model, HttpStatusCode code)
        {
            JsonResult jsonResult = new JsonResult(model)
            {
                StatusCode = (int)code,
                SerializerSettings = new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateParseHandling = DateParseHandling.DateTime,

                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            };

            return jsonResult;
        }

        protected IActionResult Created<T>(T model) => this.JsonResult<T>(model, HttpStatusCode.Created);

        protected IActionResult Ok() => this.Result(HttpStatusCode.OK);
        
        protected IActionResult Ok<T>(T model) => this.JsonResult<T>(model, HttpStatusCode.OK);

        protected IActionResult BadRequest<T>(T model) => this.JsonResult<T>(model, HttpStatusCode.BadRequest);

        protected IActionResult NotFound() => this.Result(HttpStatusCode.NotFound);

        protected IActionResult ServerError() => this.Result(HttpStatusCode.InternalServerError);
        
        protected IActionResult ServiceUnavailable() => this.Result(HttpStatusCode.ServiceUnavailable);

    }
}