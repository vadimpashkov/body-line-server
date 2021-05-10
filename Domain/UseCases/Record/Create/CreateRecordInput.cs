using System;
using Domain.Abstractions.Mediator;
using Domain.Extensions;
using FluentValidation;

namespace Domain.UseCases.Record.Create
{
    public class CreateRecordInput: IUseCaseInput
    {
        public int MesseurId { get; set; }
        public int MassageTypeId { get; set; }
        public DateTime Date { get; set; }
    }
    public class CreateRecordInputValidator: AbstractValidator<CreateRecordInput>
    {
        public CreateRecordInputValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty();

            RuleFor(x => x.MassageTypeId)
                .EntityReference();

            RuleFor(x => x.MesseurId)
                .EntityReference();
        }
    }
}