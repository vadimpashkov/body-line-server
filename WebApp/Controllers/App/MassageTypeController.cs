using System.Threading.Tasks;
using Domain.UseCases.MassageType.Get;
using Microsoft.AspNetCore.Mvc;
using Schedule.Implementations;

namespace WebApp.Controllers.App
{
    [Route("api/v1/massagetypes")]
    public class MassageTypeController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public MassageTypeController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetTypes() => 
            await _dispatcher.DispatchAsync(new GetMassageTypesInput());
    }
}