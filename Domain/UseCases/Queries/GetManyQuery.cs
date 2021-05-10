using System.Collections.Generic;

namespace Domain.UseCases.Queries
{
    public class GetManyQuery<TFilter>
    {
        public IList<int> Ids { get; set; }

        public string FilterString { get; set; }
        public TFilter Filters { get; set; }

        public int? Start { get; set; }
        public int? End { get; set; }

        public string OrderBy { get; set; }
        public SortOrder? SortOrder { get; set; }

        public bool IsSortingEmpty()
        {
            return string.IsNullOrEmpty(OrderBy);
        }

        public bool IsPaginationEmpty()
        {
            return !Start.HasValue ||
                   !End.HasValue ||
                   !(Start.Value >= 0) ||
                   !(End.Value >= 0) ||
                   !(End.Value > Start.Value);
        }
    }

    public enum SortOrder
    {
        Asc,
        Desc,
    }
}
