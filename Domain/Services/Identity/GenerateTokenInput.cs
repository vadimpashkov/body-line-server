using Domain.Entities;
using MediatR;

namespace Domain.Services.Identity
{
    public class GenerateTokenInput: IRequest<string>
    {
        public User User { get; set; }
    }
}