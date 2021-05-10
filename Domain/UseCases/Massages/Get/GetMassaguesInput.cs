using Domain.Abstractions.Mediator;
using FluentValidation;

namespace Domain.UseCases.Massages.Get
{
    public class GetMassaguesInput: IUseCaseInput
    {
    }
    public class GetMassaguesInputValidator: AbstractValidator<GetMassaguesInput>
    {
        public GetMassaguesInputValidator()
        {
            
        }
    }
}