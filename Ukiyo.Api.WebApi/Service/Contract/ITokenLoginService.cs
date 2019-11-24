using System;
using System.Threading.Tasks;
using Ukiyo.Api.WebApi.Identity;

namespace Ukiyo.Api.WebApi.Service.Contract
{
    public interface ITokenLoginService
    {
        Task<string> GenerateToken(string userId);
        Task<LoginToken> RecoverLoginToken(Guid token);
    }
}