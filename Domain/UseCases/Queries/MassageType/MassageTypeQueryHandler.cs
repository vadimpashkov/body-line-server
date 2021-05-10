using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Data;

namespace Domain.UseCases.Queries.MassageType
{
    public class MassageTypeQueryHandler : QueryHandler<MassageTypeViewModel, Entities.MassageType, MassageTypeViewModel>
    {
        public MassageTypeQueryHandler(IAppContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected async override Task<IQueryable<Entities.MassageType>> Filter(IQueryable<Entities.MassageType> query, MassageTypeViewModel filter)
        {
            return query;
        }
    }
}