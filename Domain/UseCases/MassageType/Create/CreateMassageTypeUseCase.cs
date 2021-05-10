using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Queries;
using Domain.Services;

namespace Domain.UseCases.MassageType.Create
{
    public class CreateMassageTypeUseCase : IUseCase<CreateMassageTypeInput>
    {
        private readonly IAppContext _context;
        private readonly IFileStorage _storage;

        public CreateMassageTypeUseCase(IAppContext context, IFileStorage storage)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IOutput> Handle(CreateMassageTypeInput request, CancellationToken cancellationToken)
        {
            using var memory = new MemoryStream();
            await request.Image.CopyToAsync(memory);

            var bytes = memory.ToArray();

            var fileName = await _storage.Save(bytes);

            var massageType = new Entities.MassageType 
            {
                Image = fileName,
                Price = request.Price,
                Name = request.Name,
                Description = request.Description,
            };

            _context.MassagesType.Add(massageType);
            await _context.SaveChangesAsync();

            return ObjectOutput.CreateWithId(massageType.Id);
        }
    }
}