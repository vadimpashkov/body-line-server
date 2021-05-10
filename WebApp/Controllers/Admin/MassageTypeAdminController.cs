using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.UseCases.MassageType.Create;
using Domain.UseCases.MassageType.Update;
using Domain.UseCases.Queries;
using Domain.UseCases.Queries.MassageType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.Identity;
using Schedule.Implementations;
using WebApp.Abstractions;

namespace WebApp.Controllers.Admin
{
    [Route("admin/api/v1/masseurtype"), AuthorizeByRoles(UserRole.Admin, UserRole.Employee)]
    public class MassageTypeAdminController : QueryController<MassageTypeViewModel, Domain.Entities.MassageType, MassageTypeViewModel>
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public MassageTypeAdminController(QueryHandler<MassageTypeViewModel, MassageType, MassageTypeViewModel> queryHandler, IUseCaseDispatcher dispatcher)
            : base(queryHandler)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateTypeForm request) =>
            await _dispatcher.DispatchAsync(new CreateMassageTypeInput 
            {
                Image = request.File.OpenReadStream(),
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync( [FromRoute] int id, [FromForm] UpdateTypeForm request) =>
            await _dispatcher.DispatchAsync(new UpdateMassagueTypeInput
            {
                Id = id,
                File = request.File == null ? Stream.Null : request.File.OpenReadStream(),
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
            });
    }

    public class UpdateTypeForm 
    {
        public IFormFile? File { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }

    public class CreateTypeForm 
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}