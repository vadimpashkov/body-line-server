using System.IO;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IFileStorage
    {
        Task<string> Save(byte[] file);
        Task<byte[]> Get(string fileName);
    }
}