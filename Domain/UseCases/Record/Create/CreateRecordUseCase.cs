using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Record.Create
{
    public class CreateRecordUseCase : IUseCase<CreateRecordInput>
    {
        private readonly IAppContext _context;
        private readonly IMediator _mediator;

        public CreateRecordUseCase(IAppContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IOutput> Handle(CreateRecordInput request, CancellationToken cancellationToken)
        {
            var currentUser = await _mediator.Send(new GetCurrentUserInput());

            var record = new Domain.Entities.Record
            {
                UserId = currentUser.Id,
                MesseurId = request.MesseurId,
                MassageTypeId = request.MassageTypeId,
                Date = request.Date,
                CreatedAt = DateTime.Now,
            };

            _context.Records.Add(record);
            await _context.SaveChangesAsync(cancellationToken);

            var addedRecord = await _context.Records
              .Include(x => x.Messeur)
              .ThenInclude(x => x.User)
              .Include(x => x.MassageType)
              .FirstAsync(x => x.Id == record.Id);


            return ActionOutput.SuccessData(new
              {
                  addedRecord.Id,
                  MasseurFirstName = addedRecord.Messeur.User.FirstName,
                  MasseurLastName = addedRecord.Messeur.User.LastName,
                  MassageTypeName = addedRecord.MassageType.Name,
                  addedRecord.Date,
                  addedRecord.CreatedAt
              });
        }
    }
}
