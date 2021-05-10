using Domain.Abstractions.Mediator;
using Domain.Extensions;
using FluentValidation;
using System.IO;

namespace Domain.UseCases.Massages.Create
{
    public class CreateMassagueInput: IUseCaseInput
    {
        public int UserId { get; set; }
        public string Occupation { get; set; }
        public string Description { get; set; }
        public Stream Photo { get; set; }
    }

    public class CreateMassagueInputValidator: AbstractValidator<CreateMassagueInput>
    {
        public CreateMassagueInputValidator()
        {
            RuleFor(x => x.UserId)
                .EntityReference();

            RuleFor(x => x.Occupation)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
