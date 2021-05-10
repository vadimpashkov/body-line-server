using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/v1/imgs")]
    public class ImgsController: Controller
    {
        private readonly IFileStorage _storage;

        public ImgsController(IFileStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetTask([FromRoute] string name) 
        {
            var bytes = await _storage.Get(name);

            return File(bytes, "image/png");
        }
    }
}