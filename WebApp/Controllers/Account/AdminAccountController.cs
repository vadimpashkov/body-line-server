using System.Threading.Tasks;
using Domain.UseCases.Account.AdminSignIn;
using Microsoft.AspNetCore.Mvc;
using Schedule.Identity;
using Schedule.Implementations;

namespace Schedule.Controllers.Account
{
    [Route("admin/api/v1/account")]
    public class AdminAccountController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public AdminAccountController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("signIn"), ValidateReCaptcha]
        public async Task<IActionResult> SignInAsync([FromBody] AdminSignInInput request) =>
            await _dispatcher.DispatchAsync(request);
    }
}