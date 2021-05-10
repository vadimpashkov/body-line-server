using Domain.Abstractions.Mediator;
using Domain.Extensions;
using FluentValidation;

namespace Domain.UseCases.Record.Cancell
{
    public class CancellRecordInput: IUseCaseInput
    {
      public int Id { get; set; }
    }

    public class CancellRecordInutValidator: AbstractValidator<CancellRecordInput>
    {
      public CancellRecordInutValidator()
      {
        RuleFor(x => x.Id)
          .EntityReference();
      }
    }
}
