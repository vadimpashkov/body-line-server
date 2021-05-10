using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;
using Domain.Entities;
using Domain.Extensions;
using Domain.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Domain.Abstractions.Outputs.ActionOutput;

namespace Domain.UseCases.Account.Login
{
    public class LoginUseCase: IUseCase<LoginInput>
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<User> _signInManager;
        private readonly IAppContext _context;
        private readonly ILogger<LoginUseCase> _logger;

        public LoginUseCase(IMediator mediator, SignInManager<User> signInManager, IAppContext context,
            ILogger<LoginUseCase> logger)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        public async Task<IOutput> Handle(LoginInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .WithRoles()
                .FirstOrDefaultAsync(x => x.UserName == request.Username, cancellationToken);

            if (user == null)
            {
                _logger.LogInformation("User {name} doesn't exists", request.Username);
                return Failure("Введен неверный номер телефона или пароль");
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!signInResult.Succeeded)
            {
                _logger.LogInformation("User {name} bad password {pass}", request.Username, request.Password);
                return Failure("Введен неверный номер телефона или пароль");
            }

            _logger.LogInformation("User {name} with password {pass} signed in", request.Username, request.Password);

            var token = await _mediator.Send(new GenerateTokenInput
            {
                User = user
            }, cancellationToken);

            return SuccessData(new
            {
                token
            });
        }
    }
}
