using Domain.Abstractions.Mediator;
using Domain.Extensions;
using FluentValidation;

namespace Domain.UseCases.Account.Login
{
    public class LoginInput: IUseCaseInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginInputValidator : AbstractValidator<LoginInput>
    {
        public LoginInputValidator()
        {
            RuleFor(x => x.Password)
                .ValidatePassword();

            RuleFor(x => x.Username)
                .ValidateUsername();
        }
    }
}