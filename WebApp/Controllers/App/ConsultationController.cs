using System.Threading.Tasks;
using Domain.UseCases.Consultation.Create;
using Microsoft.AspNetCore.Mvc;
using Schedule.Implementations;

namespace WebApp.Controllers.App
{
    [Route("api/v1/consult")]
    public class ConsultationController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public ConsultationController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateConsultationInput request) =>
            await _dispatcher.DispatchAsync(request);
    }
}