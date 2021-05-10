using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Data;
using Domain.Entities;
using Domain.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Queries.Massague
{
    public class MassageQueryHandler : QueryHandler<MassagueViewModel, Entities.Masseur, MassagueViewModel>
    {
        public MassageQueryHandler(IAppContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected override IQueryable<Masseur> GetQuery()
        {
            return base.GetQuery().Include(x => x.User);
        }

        protected async override Task<IQueryable<Masseur>> Filter(IQueryable<Masseur> query, MassagueViewModel filter)
        {
            return query;
        }
    }
}