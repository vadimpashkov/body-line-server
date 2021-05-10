using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Queries;
using Domain.Services;
using Domain.UseCases.MassageType.Update;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.MassageType.Update
{
    public class UpdateMassagueTypeUseCase : IUseCase<UpdateMassagueTypeInput>
    {
        private readonly IFileStorage _storage;
        private readonly IAppContext _context;

        public UpdateMassagueTypeUseCase(IAppContext context, IFileStorage storage)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IOutput> Handle(UpdateMassagueTypeInput request, CancellationToken cancellationToken)
        {
            var masseurType = await _context.MassagesType
                .FirstAsync(x => x.Id == request.Id, cancellationToken);

            if (request.File != Stream.Null) 
            {
                using var memory = new MemoryStream();

                await request.File.CopyToAsync(memory);
                var bytes = memory.ToArray();

                var fileName = await _storage.Save(bytes);
                masseurType.Image = fileName;
                await _context.SaveChangesAsync(cancellationToken);
            }

            masseurType.Update(request.Name, request.Price, request.Description);

            await _context.SaveChangesAsync(cancellationToken);

            return ObjectOutput.CreateWithId(masseurType.Id);
        }
    }
}