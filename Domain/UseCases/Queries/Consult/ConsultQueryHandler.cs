using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Data;

namespace Domain.UseCases.Queries.Consult
{
    public class ConsultQueryHandler : QueryHandler<ConsultViewModel, Entities.Consultation, ConsultViewModel>
    {
        public ConsultQueryHandler(IAppContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected async override Task<IQueryable<Entities.Consultation>> Filter(IQueryable<Entities.Consultation> query, ConsultViewModel filter)
        {
            return query;
        }
    }
}