using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Massages.Get
{
    public class GetMassaguesUseCase : IUseCase<GetMassaguesInput>
    {
        private readonly IAppContext _context;

        public GetMassaguesUseCase(IAppContext context)
        {
            _context = context;
        }

        public async Task<IOutput> Handle(GetMassaguesInput request, CancellationToken cancellationToken)
        {
            var massagues = await _context.Massaures
                .Include(x => x.User)
                .Select(x => new
                {
                    x.Id,
                    x.Description,
                    x.Occupation,
                    x.User.Src,
                    x.User.FirstName,
                    x.User.LastName,
                })
                .ToListAsync();

            return ActionOutput.SuccessData(massagues);
        }
    }
}
