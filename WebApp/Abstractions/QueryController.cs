using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Queries;
using Domain.Extensions;
using Domain.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Abstractions
{
    public class QueryController<TFilter, TEntity, TOutput> : ControllerBase
        where TEntity : class, IEntity
        where TFilter : class, IFilter
        where TOutput : class, IQueryOutput
    {
        protected readonly QueryHandler<TFilter, TEntity, TOutput> QueryHandler;

        public QueryController(QueryHandler<TFilter, TEntity, TOutput> queryHandler)
        {
            QueryHandler = queryHandler;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            return Present(await QueryHandler.GetById(id));
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> Get([FromQuery] Sorting sorting = null, [FromQuery] Paging paging = null,
            [FromQuery] TFilter viewModel = null, [FromQuery] string q = null, [FromQuery] int[] id = null)
        {
            var request = new GetManyQuery<TFilter>
            {
                Ids = id,
                FilterString = q,
                Filters = viewModel,
                OrderBy = sorting?._sort.Capitalize(),
                SortOrder = sorting?._order,
                Start = paging?._start,
                End = paging?._end,
            };

            var result = await QueryHandler.GetMany(request);

            Response.Headers.Add("X-Total-Count", result.TotalCount.ToString());

            return Present(result.Content);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int? id)
        {
            if (id != null)
            {
                await QueryHandler.Delete((int)id);
            }

            return Ok(new { id = id });
        }

        protected virtual IActionResult Present(TOutput output)
        {
            return Ok(output);
        }

        protected virtual IActionResult Present(IList<TOutput> output)
        {
            return Ok(output);
        }

        public class Sorting
        {
            public string _sort { get; set; }
            public SortOrder? _order { get; set; }
        }

        public class Paging
        {
            public int? _start { get; set; }
            public int? _end { get; set; }
        }
    }
}
