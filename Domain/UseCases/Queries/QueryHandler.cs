using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Data;
using Domain.Abstractions.Queries;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Domain.UseCases.Queries
{
    public abstract class QueryHandler<TFilter, TEntity, TOutput>
        where TEntity : class, IEntity
        where TFilter: class, IFilter
        where TOutput: class, IQueryOutput
    {
        protected readonly IAppContext DbContext;
        protected readonly IMapper Mapper;

        protected QueryHandler(IAppContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public async Task<TOutput> GetById(int id)
        {
            var query = GetQuery();
            var result = await query
                .FirstOrDefaultAsync(e => e.Id == id);
            
            return ToOutput(result);
        }

        public async Task<GetManyResult<TOutput>> GetMany(GetManyQuery<TFilter> q)
        {
            var query = GetQuery();

            query = FilterByIds(query, q.Ids);
            query = await Filter(query, q.Filters);
            query = Sort(query, q);

            return new GetManyResult<TOutput>(await query.CountAsync(),
                (await Paginate(query, q).ToListAsync())
                .Select(ToOutput)
                .ToList());
        }

        public async Task Delete(int id)
        {
            await DbContext.Set<TEntity>()
                .Where(x => x.Id == id)
                .DeleteAsync();
        }

        public async Task<TOutput> Update(TOutput request)
        {
            var mappedRequest = Mapper.Map<TEntity>(request);

            var found = await DbContext.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == mappedRequest.Id);

            if (found == null)
            {
                return Mapper.Map<TOutput>(mappedRequest);
            }
            
            DbContext.Entry(found).CurrentValues.SetValues(mappedRequest);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<TOutput>(mappedRequest);
        }

        public async Task<TOutput> Create(TOutput request)
        {
            var entity = Mapper.Map<TEntity>(request);

            await DbContext.Set<TEntity>()
                .SingleInsertAsync(entity);

            return Mapper.Map<TOutput>(entity);
        }
        
        protected virtual IQueryable<TEntity> GetQuery() => DbContext.Set<TEntity>();
        protected virtual TOutput ToOutput(TEntity entity) => Mapper.Map<TOutput>(entity);

        protected abstract Task<IQueryable<TEntity>> Filter(IQueryable<TEntity> query, TFilter filter);

        protected virtual IQueryable<TEntity> FilterByIds(IQueryable<TEntity> query, IList<int> ids)
        {
            if (ids != null && ids.Count != 0)
                return query.Where(e => ids.Contains(e.Id));
            
            return query;
        }

        protected virtual IQueryable<TEntity> Sort(IQueryable<TEntity> query, GetManyQuery<TFilter> request)
        {
            if (!request.IsSortingEmpty())
                return request.SortOrder == SortOrder.Desc
                    ? query.OrderByDesc(request.OrderBy)
                    : query.OrderBy(request.OrderBy);
            
            return query.OrderBy(e => e.Id);
        }

        protected virtual IQueryable<T> Paginate<T>(IQueryable<T> query, GetManyQuery<TFilter> request)
        {
            if (!request.IsPaginationEmpty())
                return query
                    .Skip(request.Start.Value)
                    .Take(request.End.Value - request.Start.Value);
            
            return query;
        }
    }
}