﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Data;
using Domain.Entities;
using Domain.Extensions;
using Domain.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Identity
{
    public class CurrentUserGetter : IRequestHandler<GetCurrentUserInput, User>
    {
        private readonly IAppContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserGetter(IAppContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> Handle(GetCurrentUserInput request, CancellationToken cancellationToken)
        {
            var includeExpression = request.IncludeExpression;
            
            var query = includeExpression != null ? includeExpression(_dbContext.Users) : _dbContext.Users.WithRoles();

            var id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(id))
                return null;

            if (!int.TryParse(id, out var intId))
                return null;

            return await query.FirstOrDefaultAsync(u => u.Id == intId, cancellationToken);
        }
    }
}
