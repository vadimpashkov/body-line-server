using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Services;

namespace Infrastructure.Services
{
    public class FileStorage : IFileStorage
    {
        private readonly string _dir;

        public FileStorage()
        {
            _dir = Path.Combine(Directory.GetCurrentDirectory(), "imgs");

            if (!Directory.Exists(_dir)) 
            {
                Directory.CreateDirectory(_dir);
            } 
        }

        public async Task<byte[]> Get(string fileName)
        {
            try {
                var bytes = await File.ReadAllBytesAsync(Path.Combine(_dir, fileName));

                return bytes;
            }
            catch {
                return new byte[0];
            }
        }

        public async Task<string> Save(byte[] file)
        {
            var fileName = Guid.NewGuid().ToString("N") + ".jpg";

            await File.WriteAllBytesAsync(Path.Combine(_dir, fileName), file);

            return fileName;
        }
    }
}