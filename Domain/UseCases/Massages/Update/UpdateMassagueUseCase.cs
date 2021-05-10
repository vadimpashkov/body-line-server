using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Queries;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Massages.Update
{
    public class UpdateMassagueUseCase : IUseCase<UpdateMassagueInput>
    {
        private readonly IFileStorage _storage;
        private readonly IAppContext _context;

        public UpdateMassagueUseCase(IAppContext context, IFileStorage storage)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IOutput> Handle(UpdateMassagueInput request, CancellationToken cancellationToken)
        {
            var masseur = await _context.Massaures
                .Include(x => x.User)
                .FirstAsync(x => x.Id == request.Id, cancellationToken);

            if (request.File != Stream.Null) 
            {
                using var memory = new MemoryStream();

                await request.File.CopyToAsync(memory);
                var bytes = memory.ToArray();

                var fileName = await _storage.Save(bytes);
                masseur.User.SetSrc(fileName);
                await _context.SaveChangesAsync(cancellationToken);
            }

            masseur.Update(request.Occupation, request.Description, request.FirstName, request.LastName);

            await _context.SaveChangesAsync(cancellationToken);

            return ObjectOutput.CreateWithId(masseur.Id);
        }
    }
}