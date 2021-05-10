using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Record.Get
{
  public class GetRecordUseCase : IUseCase<GetRecordInput>
  {
    private readonly IAppContext _context;
    private readonly IMediator _mediator;

    public GetRecordUseCase(IAppContext context, IMediator mediator)
    {
      _context = context;
      _mediator = mediator;
    }

    public async Task<IOutput> Handle(GetRecordInput request, CancellationToken cancellationToken)
    {
      var user = await _mediator.Send(new GetCurrentUserInput());

      var records = await _context.Records
        .Include(x => x.Messeur)
          .ThenInclude(x => x.User)
        .Include(x => x.MassageType)
        .Where(x => x.UserId == user.Id)
        .Where(x => !x.Cancelled)
        .OrderByDescending(x => x.CreatedAt)
        .Select(x => new
        {
          x.Id,
          MasseurFirstName = x.Messeur.User.FirstName,
          MasseurLastName = x.Messeur.User.LastName,
          MassageTypeName = x.MassageType.Name,
          x.Date,
          x.CreatedAt
        })
        .ToListAsync();

      return ActionOutput.SuccessData(records);
    }
  }
}
