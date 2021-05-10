using Domain.Entities;
using Domain.Enums;
using Domain.UseCases.Queries;
using Domain.UseCases.Queries.Users;
using Microsoft.AspNetCore.Mvc;
using Schedule.Identity;
using WebApp.Abstractions;

namespace WebApp.Controllers.Admin
{
    [Route("admin/api/v1/users"), AuthorizeByRoles(UserRole.Admin, UserRole.Employee)]
    public class UserAdminController : QueryController<UserViewModel, Domain.Entities.User, UserViewModel>
    {
        public UserAdminController(QueryHandler<UserViewModel, User, UserViewModel> queryHandler) : base(queryHandler)
        {
        }
    }
}