using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Queries;
using Domain.Entities;
using Domain.Services;

namespace Domain.UseCases.Pictures.Create
{
    public class CreatePictureUseCase : IUseCase<CreatePictureInput>
    {
        private readonly IAppContext _context;
        private readonly IFileStorage _storage;

        public CreatePictureUseCase(IAppContext context, IFileStorage storage)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IOutput> Handle(CreatePictureInput request, CancellationToken cancellationToken)
        {
            using var memory = new MemoryStream();

            await request.File.CopyToAsync(memory);

            var bytes = memory.ToArray();

            var fileName = await _storage.Save(bytes);

            var img = new Photos 
            {
                Src = fileName,
                Alt = request.Alt,
            };

            _context.Photos.Add(img);

            await _context.SaveChangesAsync();

            return ObjectOutput.CreateWithId(img.Id);
        }
    }
}