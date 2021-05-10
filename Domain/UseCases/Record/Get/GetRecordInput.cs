using Domain.Abstractions.Mediator;
using FluentValidation;

namespace Domain.UseCases.Record.Get
{
    public class GetRecordInput: IUseCaseInput
    {

    }

    public class GetRecordInutValidator: AbstractValidator<GetRecordInput>
    {
      public GetRecordInutValidator()
      {

      }
    }
}
