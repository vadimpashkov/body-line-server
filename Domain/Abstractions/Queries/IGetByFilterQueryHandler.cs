using System.Collections.Generic;

namespace Domain.Abstractions.Queries
{
    public interface IGetByFilterQueryHandler<TFilter, TOutput> : IQueryHandler<TFilter, IList<TOutput>>
    {
    }
}
