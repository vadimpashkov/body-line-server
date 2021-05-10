using Domain.Abstractions.Mediator;
using FluentValidation;

namespace Domain.UseCases.MassageType.Get
{
    public class GetMassageTypesInput: IUseCaseInput
    {
        
    }

    public class GetMassageTypesInputValidator: AbstractValidator<GetMassageTypesInput>
    {
        public GetMassageTypesInputValidator()
        {
            
        }
    }
}