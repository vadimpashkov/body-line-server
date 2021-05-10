using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Domain.UseCases.Record.Cancell
{
  public class CancellRecordUseCase : IUseCase<CancellRecordInput>
  {
    private readonly IAppContext _context;

    public CancellRecordUseCase(IAppContext context)
    {
      _context = context;
    }

    public async Task<IOutput> Handle(CancellRecordInput request, CancellationToken cancellationToken)
    {
      var record = await _context.Records
      .Include(x => x.Messeur)
      .ThenInclude(x => x.User)
      .Include(x => x.MassageType)
      .FirstAsync(x => x.Id == request.Id);

      record.Cancelled = true;
      await _context.SaveChangesAsync();

      return ActionOutput.SuccessData(new
      {
          record.Id,
          MasseurFirstName = record.Messeur.User.FirstName,
          MasseurLastName = record.Messeur.User.LastName,
          MassageTypeName = record.MassageType.Name,
          record.Date,
          record.CreatedAt
      });
    }
  }
}
