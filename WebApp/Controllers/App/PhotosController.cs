using System.Threading.Tasks;
using Domain.UseCases.Pictures.Get;
using Microsoft.AspNetCore.Mvc;
using Schedule.Implementations;

namespace WebApp.Controllers.App
{
    [Route("api/v1/photos")]
    public class PhotosController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public PhotosController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() =>
            await _dispatcher.DispatchAsync(new GetPicturesInput());
    }
}