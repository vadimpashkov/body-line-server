using System;
using System.Linq;
using Domain.Entities;
using MediatR;

namespace Domain.Services.Identity
{
    public class GetCurrentUserInput: IRequest<User>
    {
        public Func<IQueryable<User>, IQueryable<User>> IncludeExpression { get; set; }

        public GetCurrentUserInput(Func<IQueryable<User>, IQueryable<User>> includeExpression = null)
        {
            IncludeExpression = includeExpression;
        }
    }
}