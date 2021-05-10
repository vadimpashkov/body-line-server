using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Data;
using Domain.Entities;

namespace Domain.UseCases.Queries.Pictures
{
    public class PicturesQueryHandler : QueryHandler<PicturesViewModel, Entities.Photos, PicturesViewModel>
    {
        public PicturesQueryHandler(IAppContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected async override Task<IQueryable<Photos>> Filter(IQueryable<Photos> query, PicturesViewModel filter)
        {
            return query;
        }
    }
}