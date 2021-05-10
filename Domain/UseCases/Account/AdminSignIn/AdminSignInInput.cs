using Domain.Abstractions.Mediator;
using FluentValidation;

namespace Domain.UseCases.Account.AdminSignIn
{
    public class AdminSignInInput: IUseCaseInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AdminSignInInputValidator : AbstractValidator<AdminSignInInput>
    {
        public AdminSignInInputValidator()
        {
            RuleFor(i => i.UserName).NotEmpty();
            RuleFor(i => i.Password).NotEmpty();
        }
    }
}