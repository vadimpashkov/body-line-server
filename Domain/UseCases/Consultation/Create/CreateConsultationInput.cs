using Domain.Abstractions.Mediator;
using FluentValidation;

namespace Domain.UseCases.Consultation.Create
{
    public class CreateConsultationInput: IUseCaseInput
    {
        public string Phone { get; set; }
    }

    public class CreateConsultationInputValidator : AbstractValidator<CreateConsultationInput>
    {
        public CreateConsultationInputValidator()
        {
        }
    }
}