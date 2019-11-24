using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ukiyo.Api.WebApi.Identity;
using Ukiyo.Api.WebApi.Service.Contract;
using Newtonsoft.Json;

namespace Ukiyo.Api.WebApi.Service.Implementation
{
    public class TokenLoginService : ITokenLoginService
    {
        protected readonly ILogger<TokenLoginService> Logger;
        protected readonly IConfiguration Configuration;
        protected readonly ApplicationDbContext Context;

        public TokenLoginService(ILogger<TokenLoginService> logger, IConfiguration configuration, ApplicationDbContext context)
        {
            Logger = logger;
            Configuration = configuration;
            Context = context;
        }

        public async Task<string> GenerateToken(string userId)
        {
            var token = await Context.LoginTokens.FirstOrDefaultAsync(t => t.IdentityUserId == userId);

            if (token != null)
            {
                Context.LoginTokens.Remove(token);
            }
            
            token = new LoginToken
            {
                Id = Guid.NewGuid(),
                IdentityUserId = userId
            };

            await Context.LoginTokens.AddAsync(token);
            await Context.SaveChangesAsync();
            
            var fullTokenRaw = new
            {
                Url = Configuration["WEBAPI_URL"],
                Token = token.Id
            };

            var fullTokenParsed = JsonConvert.SerializeObject(fullTokenRaw);
            var encodedBytes = System.Text.Encoding.UTF8.GetBytes(fullTokenParsed);
            var fullTokenEncoded = Convert.ToBase64String(encodedBytes);

            return fullTokenEncoded;
        }

        public async Task<LoginToken> RecoverLoginToken(Guid token)
        {
            var loginToken = await Context.LoginTokens.FirstOrDefaultAsync(t => t.Id == token);
            
            return loginToken;
        }
    }
}