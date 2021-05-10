using Domain.Abstractions.Mediator;
using FluentValidation;

namespace Domain.UseCases.Pictures.Get
{
    public class GetPicturesInput: IUseCaseInput
    {
        
    }
    public class GetPicturesInputValidator: AbstractValidator<GetPicturesInput>
    {
        public GetPicturesInputValidator()
        {
            
        }
    }
}