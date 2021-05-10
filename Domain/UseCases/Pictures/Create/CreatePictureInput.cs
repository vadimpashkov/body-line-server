using System.IO;
using Domain.Abstractions.Mediator;
using FluentValidation;

namespace Domain.UseCases.Pictures.Create
{
    public class CreatePictureInput: IUseCaseInput
    {
        public Stream File { get; set; }
        public string Alt { get; set; }
    }

    public class CreatePictureInputValidator: AbstractValidator<CreatePictureInput> 
    {
        public CreatePictureInputValidator()
        {
            
        }
    }
}