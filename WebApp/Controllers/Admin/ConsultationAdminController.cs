using Domain.Entities;
using Domain.Enums;
using Domain.UseCases.Queries;
using Domain.UseCases.Queries.Consult;
using Microsoft.AspNetCore.Mvc;
using Schedule.Identity;
using WebApp.Abstractions;

namespace WebApp.Controllers.Admin
{
    [Route("admin/api/v1/consult"), AuthorizeByRoles(UserRole.Admin, UserRole.Employee)]
    public class ConsultationAdminController : QueryController<ConsultViewModel, Domain.Entities.Consultation, ConsultViewModel>
    {
        public ConsultationAdminController(QueryHandler<ConsultViewModel, Consultation, ConsultViewModel> queryHandler) 
            : base(queryHandler)
        {
        }
    }
}