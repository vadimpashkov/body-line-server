using System.IO;
using Domain.Abstractions.Mediator;
using FluentValidation;

namespace Domain.UseCases.MassageType.Create
{
    public class CreateMassageTypeInput: IUseCaseInput
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Stream Image { get; set; }
    }

    public class CreateMassageTypeInputValidator: AbstractValidator<CreateMassageTypeInput>
    {
        public CreateMassageTypeInputValidator()
        {
            
        }
    }
}