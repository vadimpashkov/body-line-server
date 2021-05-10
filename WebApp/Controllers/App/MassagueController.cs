using System.Threading.Tasks;
using Domain.UseCases.Massages.Get;
using Microsoft.AspNetCore.Mvc;
using Schedule.Implementations;

namespace WebApp.Controllers.App
{
    [Route("api/v1/massague")]
    public class MassagueController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public MassagueController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync() =>
            await _dispatcher.DispatchAsync(new GetMassaguesInput());
    }
}