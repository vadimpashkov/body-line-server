using System.Threading.Tasks;

namespace Domain.Abstractions.Queries
{
    public interface IQueryHandler<TQuery, TOutput>
    {
        Task<TOutput> HandleAsync(TQuery query);
    }
}
