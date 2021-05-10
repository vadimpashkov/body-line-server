using System.Threading.Tasks;
using Domain.Abstractions.Outputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Presentation.Output;
using WebApp.Identity;

namespace Schedule.Identity
{
    public class ValidateReCaptchaAttribute : ActionFilterAttribute
    {
        private const string CaptchaResponseHeader = "CaptchaResponse";

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var headerValue = context.HttpContext.Request.Headers[CaptchaResponseHeader].ToString();
            if (string.IsNullOrEmpty(headerValue))
            {
                context.Result = FailureResult;
                return;
            }

            var validator = context.HttpContext.RequestServices.GetRequiredService<ReCaptchaValidator>();

            var valid = await validator.IsCaptchaPassedAsync(headerValue);
            if (!valid)
                context.Result = FailureResult;
            else
                await next();
        }

        private IActionResult FailureResult => JsonActionResult.Forbidden(ActionOutput.Failure("invalid captcha response"));
    }
}
