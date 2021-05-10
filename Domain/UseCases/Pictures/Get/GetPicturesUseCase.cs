using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Pictures.Get
{
    public class GetPicturesUseCase : IUseCase<GetPicturesInput>
    {
        private readonly IAppContext _context;

        public GetPicturesUseCase(IAppContext context)
        {
            _context = context;
        }
        public async Task<IOutput> Handle(GetPicturesInput request, CancellationToken cancellationToken)
        {
            var pics = await _context.Photos
                .Select(x => new
                {
                    x.Id,
                    x.Src,
                    x.Alt
                })
                .ToListAsync();

            return ActionOutput.SuccessData(pics);
        }
    }
}
