using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Ukiyo.Api.WebApi.Service.Contract;

namespace Ukiyo.Api.WebApi.Service.Implementation
{
    public class JwtTokenService : IJwtTokenService
    {
        protected readonly ILogger<JwtTokenService> Logger;
        protected readonly IConfiguration Configuration;
        protected readonly UserManager<IdentityUser> UserManager;

        public JwtTokenService(ILogger<JwtTokenService> logger, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            Logger = logger;
            Configuration = configuration;
            UserManager = userManager;
        }

        public async Task<JwtToken> Generate(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT_TOKEN_SECURITY_KEY"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["JWT_TOKEN_ISSUER"],
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }

    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}