using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Schedule.Presentation.Output
{
    public class JsonActionResult: ContentResult
    {
        public JsonActionResult(object response)
            : this(JsonSerializationHelper.Serialize(response))
        {
        }

        public JsonActionResult(string response)
            : this()
        {
            Content = response;
        }

        protected JsonActionResult()
        {
            ContentType = "application/json";
        }

        public static ActionResult Ok(object content) => new JsonActionResult(content);
        public static ActionResult NotFound(object content) => HttpCodeResponse(content, HttpStatusCode.NotFound);
        public static ActionResult BadRequest(object content) => HttpCodeResponse(content, HttpStatusCode.BadRequest);
        public static ActionResult Unauthorized(object content) => HttpCodeResponse(content, HttpStatusCode.Unauthorized);
        public static ActionResult Forbidden(object content) => HttpCodeResponse(content, HttpStatusCode.Forbidden);

        private static ActionResult HttpCodeResponse(object content, HttpStatusCode code) => new JsonActionResult(content) { StatusCode = (int)code };

    }
}