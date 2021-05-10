using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Extensions;
using Domain.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Domain.Abstractions.Outputs.ActionOutput;

namespace Domain.UseCases.Account.AdminSignIn
{
    public class AdminSignInUseCase: IUseCase<AdminSignInInput>
    {
        private readonly ILogger<AdminSignInUseCase> _logger;
        private readonly IAppContext _context;
        private readonly SignInManager<Entities.User> _signInManager;
        private readonly IMediator _mediator;

        public AdminSignInUseCase(ILogger<AdminSignInUseCase> logger, IAppContext context, SignInManager<Entities.User> signInManager, IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _mediator = mediator;
        }

        public async Task<IOutput> Handle(AdminSignInInput request, CancellationToken cancellationToken)
        {
            var userName = request.UserName.Trim().ToLower();
            var user = await _context.Users.WithRoles()
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == userName, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("User {UserName} not found", request.UserName);

                return Failure("Введен неверный номер телефона или пароль");
            }

            if (!user.IsStaff)
            {
                _logger.LogWarning("User {UserName} try to sign in as admin", request.UserName);

                return Failure("Нет доступа");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                _logger.LogWarning("User {UserName} try to sign in as admin with password {Password}", request.UserName, request.Password);

                return Failure("Не найден");
            }

            _logger.LogInformation("User {UserName} sign in as admin", request.UserName);

            return SuccessData(new
            {
                AccessToken = await _mediator.Send(new GenerateTokenInput {User = user}, cancellationToken),
                Roles = user.Roles.Select(r => r.ToString()).ToList(),
            });
        }
    }
}
