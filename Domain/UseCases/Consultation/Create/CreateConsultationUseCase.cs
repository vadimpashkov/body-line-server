using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Abstractions.Mediator;
using Domain.Abstractions.Outputs;

namespace Domain.UseCases.Consultation.Create
{
    public class CreateConsultationUseCase : IUseCase<CreateConsultationInput>
    {
        private readonly IAppContext _context;

        public CreateConsultationUseCase(IAppContext context)
        {
            _context = context;
        }

        public async Task<IOutput> Handle(CreateConsultationInput request, CancellationToken cancellationToken)
        {
            var consult = new Entities.Consultation 
            {
                Phone = request.Phone,
                DateCreation = DateTime.Now,
            };

            _context.Consultations.Add(consult);

            await _context.SaveChangesAsync(cancellationToken);

            return ActionOutput.Success;
        }
    }
}