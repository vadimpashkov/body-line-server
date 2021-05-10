using System;
using System.Threading.Tasks;

namespace Domain.Abstractions.Data
{
    public interface IUnitOfWork: IDisposable
    {
        Task Apply();
        Task Cancel();
    }
}