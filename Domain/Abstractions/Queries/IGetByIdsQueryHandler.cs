using System.Collections.Generic;

namespace Domain.Abstractions.Queries
{
    public interface IGetByIdsQueryHandler<TOutput> : IQueryHandler<IList<int>, IList<TOutput>>
    {
    }
}
