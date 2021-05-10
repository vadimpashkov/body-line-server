using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Queries.Users
{
    public class UserQueryHandler : QueryHandler<UserViewModel, Entities.User, UserViewModel>
    {
        public UserQueryHandler(IAppContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected async override Task<IQueryable<User>> Filter(IQueryable<User> query, UserViewModel filter)
        {
            var tempQuery = query;

            if (!string.IsNullOrEmpty(filter.q)) 
            {
                tempQuery = tempQuery.Where(x => x.UserName.ToLower().Contains(filter.q.ToLower()));
            }

            return tempQuery.Include(x => x.RolesEntities)
                .Where(x => !x.RolesEntities
                    .Any(x => 
                        x.Role.Name == Enums.UserRole.Admin.ToString() || 
                        x.Role.Name == Enums.UserRole.Employee.ToString())
                    );
        }
    }
}