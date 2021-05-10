using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.UseCases.Pictures.Create;
using Domain.UseCases.Queries;
using Domain.UseCases.Queries.Pictures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.Identity;
using Schedule.Implementations;
using WebApp.Abstractions;

namespace WebApp.Controllers.Admin
{
    [Route("admin/api/v1/imgs"), AuthorizeByRoles(UserRole.Admin, UserRole.Employee)]
    public class PhotosAdminController : QueryController<PicturesViewModel, Domain.Entities.Photos, PicturesViewModel>
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public PhotosAdminController(QueryHandler<PicturesViewModel, Photos, PicturesViewModel> queryHandler, IUseCaseDispatcher dispatcher)
            : base(queryHandler)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] PhotoCreationModel request) =>
            await _dispatcher.DispatchAsync(new CreatePictureInput
            {
                File = request.File.OpenReadStream(),
                Alt = request.Alt
            });
    }

    public class PhotoCreationModel 
    {
        public IFormFile File { get; set; }
        public string Alt { get; set; }
    }
}