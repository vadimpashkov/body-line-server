using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using Domain.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Domain.Abstractions.Outputs.ActionOutput;

namespace Domain.UseCases.Account.Register
{
    public class RegisterUseCase: IUseCase<RegisterInput>
    {
        private readonly IDateTimeProvider _provider;
        private readonly UserManager<Entities.User> _userManager;
        private readonly IMediator _mediator;
        private readonly IAppContext _context;
        private readonly ILogger<RegisterUseCase> _logger;

        public RegisterUseCase(IDateTimeProvider provider, UserManager<Entities.User> userManager, IMediator mediator,
            IAppContext context, ILogger<RegisterUseCase> logger)
        {
            _provider = provider;
            _userManager = userManager;
            _mediator = mediator;
            _context = context;
            _logger = logger;
        }

        public async Task<IOutput> Handle(RegisterInput request, CancellationToken cancellationToken)
        {
            var user = new Entities.User(_provider.Now, request.Username, request.FirstName, request.LastName);

            var registerResult = await _userManager.CreateAsync(user);

            if (!registerResult.Succeeded)
            {
                return Failure("Пользователь с таким номером телефона уже существует");
            }

            await _userManager.AddToRoleAsync(user, UserRole.Participant.ToString());
            await _userManager.AddPasswordAsync(user, request.Password);

            var userWithRoles = await _context.Users
                .WithRoles()
                .FirstAsync(x => x.Id == user.Id, cancellationToken);

            _logger.LogInformation("User {user} registered with pass {pass}", request.Username, request.Password);

            var token = await _mediator.Send(new GenerateTokenInput
            {
                User = userWithRoles,
            }, cancellationToken);

            return SuccessData(new
            {
                token
            });
        }
    }
}
