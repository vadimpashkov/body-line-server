using System.Threading.Tasks;
using Domain.Enums;
using Domain.UseCases.Record.Cancell;
using Domain.UseCases.Record.Create;
using Domain.UseCases.Record.Get;
using Microsoft.AspNetCore.Mvc;
using Schedule.Identity;
using Schedule.Implementations;

namespace WebApp.Controllers.App
{
    [Route("api/v1/record"), AuthorizeByRoles(UserRole.Participant)]
    public class RecordController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public RecordController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> RecordAsync([FromBody] CreateRecordInput request) =>
            await _dispatcher.DispatchAsync(request);

        [HttpGet]
        public async Task<IActionResult> GetRecordsAsync() =>
          await _dispatcher.DispatchAsync(new GetRecordInput());

        [HttpPost("{id}")]
        public async Task<IActionResult> CancellRecordsAsync([FromRoute] int id) =>
          await _dispatcher.DispatchAsync(new CancellRecordInput
          {
            Id = id
          });
    }
}
