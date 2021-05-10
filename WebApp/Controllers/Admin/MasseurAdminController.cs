using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.UseCases.Massages.Create;
using Domain.UseCases.Massages.Update;
using Domain.UseCases.Queries;
using Domain.UseCases.Queries.Massague;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.Identity;
using Schedule.Implementations;
using WebApp.Abstractions;

namespace WebApp.Controllers.Admin
{
    [Route("admin/api/v1/masseur"), AuthorizeByRoles(UserRole.Admin, UserRole.Employee)]
    public class MasseurAdminController : QueryController<MassagueViewModel, Masseur, MassagueViewModel>
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public MasseurAdminController(QueryHandler<MassagueViewModel, Masseur, MassagueViewModel> queryHandler, IUseCaseDispatcher dispatcher)
            : base(queryHandler)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateForm request) =>
            await _dispatcher.DispatchAsync(new CreateMassagueInput
            {
              Photo = request.File.OpenReadStream(),
              Occupation = request.Occupation,
              Description = request.Description,
              UserId = request.UserId,
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync( [FromRoute] int id, [FromForm] UpdateForm request) =>
            await _dispatcher.DispatchAsync(new UpdateMassagueInput
            {
                Id = id,
                File = request.File == null ? Stream.Null : request.File.OpenReadStream(),
                Occupation = request.Occupation,
                Description = request.Description,
                FirstName = request.FirstName,
                LastName = request.LastName,
            });
    }

    public class UpdateForm
    {
        public IFormFile? File { get; set; }
        public string Occupation { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CreateForm
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }
        public string Occupation { get; set; }
        public string Description { get; set; }
    }
}
