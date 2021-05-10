using System.IO;
using Domain.Abstractions.Mediator;
using Domain.Extensions;
using FluentValidation;

namespace Domain.UseCases.Massages.Update
{
    public class UpdateMassagueInput: IUseCaseInput
    {
        public int Id { get; set; }
        public Stream File { get; set; }
        public string Occupation { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateMassagueInputValidator: AbstractValidator<UpdateMassagueInput> 
    {
        public UpdateMassagueInputValidator()
        {
            RuleFor(x => x.Id)
                .EntityReference();
        }
    }
}