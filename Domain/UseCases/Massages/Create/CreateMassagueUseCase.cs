using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Queries;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Massages.Create
{
    public class CreateMassagueUseCase : IUseCase<CreateMassagueInput>
    {
        private readonly IAppContext _context;
        private readonly IFileStorage _storage;
        private readonly UserManager<User> _manager;

      public CreateMassagueUseCase(IAppContext context, UserManager<User> manager, IFileStorage storage)
      {
        _context = context;
        _manager = manager;
        _storage = storage;
      }

    public async Task<IOutput> Handle(CreateMassagueInput request, CancellationToken cancellationToken)
        {
            var massague = new Entities.Masseur
            {
                UserId = request.UserId,
                Occupation = request.Occupation,
                Description = request.Description,
            };

            _context.Massaures.Add(massague);

            var user = await _manager.FindByIdAsync(request.UserId.ToString());

            using var memory = new MemoryStream();

            await request.Photo.CopyToAsync(memory);
            var bytes = memory.ToArray();

            var fileName = await _storage.Save(bytes);
            user.SetSrc(fileName);
            await _context.SaveChangesAsync(cancellationToken);

            await _manager.AddToRoleAsync(user, Enums.UserRole.Employee.ToString());
            await _context.SaveChangesAsync(cancellationToken);

            return ObjectOutput.CreateWithId(massague.Id);
        }
    }
}
