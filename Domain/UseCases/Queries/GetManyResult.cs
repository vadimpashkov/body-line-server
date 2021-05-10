using System.Collections.Generic;

namespace Domain.UseCases.Queries
{
    public class GetManyResult<TOutput>
    {
        public int TotalCount { get; }
        public IList<TOutput> Content { get; }

        public GetManyResult(int totalCount, IList<TOutput> content)
        {
            TotalCount = totalCount;
            Content = content;
        }
    }
}
