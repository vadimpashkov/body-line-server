using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.MassageType.Get
{
    public class GetMassageTypeUseCase : IUseCase<GetMassageTypesInput>
    {
        private readonly IAppContext _context;

        public GetMassageTypeUseCase(IAppContext context)
        {
            _context = context;
        }

        public async Task<IOutput> Handle(GetMassageTypesInput request, CancellationToken cancellationToken)
        {
            var items = await _context.MassagesType.OrderByDescending(x => x.Id).ToListAsync();

            return ActionOutput.SuccessData(items);
        }
    }
}
