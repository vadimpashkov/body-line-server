using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Data;
using Domain.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Queries.Record
{
    public class RecordQueryHandler : QueryHandler<RecordViewModel, Entities.Record, RecordViewModel>
    {
        private readonly IMediator _mediator;
        public RecordQueryHandler(IAppContext dbContext, IMapper mapper, IMediator mediator) : base(dbContext, mapper)
        {
            _mediator = mediator;
        }

        protected async override Task<IQueryable<Entities.Record>> Filter(IQueryable<Entities.Record> query, RecordViewModel filter)
        {
            var currentUser = await _mediator.Send(new GetCurrentUserInput());

            if (currentUser.Roles.Contains(Enums.UserRole.Employee)) 
            {
                return query.Where(x => x.Messeur.UserId == currentUser.Id);
            }
            return query;
        }
    }
}