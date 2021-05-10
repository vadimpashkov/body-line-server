using Domain.Entities;
using Domain.Enums;
using Domain.UseCases.Queries;
using Domain.UseCases.Queries.Record;
using Microsoft.AspNetCore.Mvc;
using Schedule.Identity;
using WebApp.Abstractions;

namespace WebApp.Controllers.Admin
{
    [Route("admin/api/v1/records"), AuthorizeByRoles(UserRole.Admin, UserRole.Employee)]
    public class RecordAdminController : QueryController<RecordViewModel, Domain.Entities.Record, RecordViewModel>
    {
        public RecordAdminController(QueryHandler<RecordViewModel, Record, RecordViewModel> queryHandler) 
            : base(queryHandler)
        {
        }
    }
}