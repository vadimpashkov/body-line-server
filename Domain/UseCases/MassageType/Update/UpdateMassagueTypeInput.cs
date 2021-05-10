using System.IO;
using Domain.Abstractions.Mediator;
using Domain.Extensions;
using FluentValidation;

namespace Domain.UseCases.MassageType.Update
{
    public class UpdateMassagueTypeInput: IUseCaseInput
    {
        public int Id { get; set; }
        public Stream File { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }

    public class UpdateMassagueTypeInputValidator: AbstractValidator<UpdateMassagueTypeInput> 
    {
        public UpdateMassagueTypeInputValidator()
        {
            RuleFor(x => x.Id)
                .EntityReference();
        }
    }
}