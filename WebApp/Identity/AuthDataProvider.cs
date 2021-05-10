using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Services.Identity;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Schedule.Configuration;

namespace Schedule.Identity
{
    public class AuthDataProvider: IRequestHandler<GenerateTokenInput, string>
    {
        private readonly IDateTimeProvider _dateTime;
        private readonly AuthConfiguration _config;

        public AuthDataProvider(IOptions<AuthConfiguration> config, IDateTimeProvider dateTime)
        {
            _dateTime = dateTime;
            _config = config.Value;
        }

        public Task<string> Handle(GenerateTokenInput request, CancellationToken cancellationToken)
        {
            var user = request.User;
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.ToString())));

            var token = new JwtSecurityToken(
                _config.Issuer,
                _config.Audience,
                claims,
                expires: _dateTime.Now.AddHours(_config.TokenLifetimeHours),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
