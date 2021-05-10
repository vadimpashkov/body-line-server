﻿using Domain.Abstractions.Outputs;
using MediatR;

namespace Domain.Abstractions.Mediator
{
    public interface IUseCaseInput: IRequest<IOutput>
    { }
}